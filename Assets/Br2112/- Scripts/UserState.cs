using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using RPC = Photon.Pun.PunRPC;

public class UserState: MonoBehaviourPunCallbacks {

	public enum State{ UNKNOWN, ENTERING, SPECTATE, IDLE, MATCHING, WIN, GHOST }
	public float spawnRadius = 20;
	public int minUsersToMatch = 3;
	public State state = State.UNKNOWN;
	public string playerName;
	[Header("Debug")]
	public int _playersLeft;

	// ------------------------------------------------------------------------

	public void RPC(string msg){
		Debug.Log("Send no-arg RPC: "+msg);
		proxy.RPC(msg, RpcTarget.All);
	}

	public void RPC(string msg, string str){
		Debug.Log("Send 1-arg RPC: "+msg+", "+str);
		proxy.RPC(msg, RpcTarget.All, str);
	}

	// ------------------------------------------------------------------------

	void Start(){
		if(!proxy.IsMine) return;
		RPC("Enter");
		InvokeRepeating("DoUpdate", 1, 1);
	}

	void SelectInitialState(){
		if(playersUnknown>0){
			print("SOME PLAYERS HAVE UNKNOWN STATUS, WAIT");
			state = State.ENTERING;
			return;
		}
		if(playersLeft>0){
			print("SOME PLAYERS ARE HAVING A MATCH; SPECTATE");
			RPC("EnterSpectate");
			OnSpectate();
		}else{
			print("NO MATCH STARTED, LET'S IDLE");
			RPC("EnterIdle");
		}
	}

	void Update(){
		_playersLeft = playersLeft;
	}

	override public void OnPlayerEnteredRoom(Player player){
		proxy.RPC("StateUpdate", player, (int)state);
	}

	void DoUpdate(){
		if(!proxy.IsMine) return;
		UpdateUI();
		UpdateName();
		if(winner!=null) return;
		switch(state){
			case State.ENTERING:
				SelectInitialState();
				return;
			case State.IDLE:
				if(usersEntering>0 || playersUnknown>0){
					print("Some have entering state; stick to idle for now");
					return;
				}
				if(userCount>=minUsersToMatch) RPC("EnterMatch");
				break;
			case State.SPECTATE:
				if(matchOver) RPC("EnterIdle");
				break;
			case State.MATCHING:
				if(idleUsers>0) return;
				if(KO) RPC("Lose");
				//if(playersLeft==1) {
				if(playersLeft==1 && !KO) {
					print("I am the winner");
					RPC("Win", UserName.value);
				}
				break;
			case State.GHOST:
			 	if(matchOver) RPC("EnterIdle");
				break;
		}
		//GetComponentInChildren<Billboard>().Display(state.ToString());
	}

	void UpdateName(){
		var x = UserName.value;
		if(x!=playerName){
			RPC("NameTag", x);
		}
	}

	void UpdateUI(){
		var ui = this.Find<Text>("Match Status Text");
		string n;
		var p = playersLeft;
		if(p==0){
			n = string.Format("Awaiting {0} more user(s) for match",
			                  minUsersToMatch-idleUsers);
		}else{
			n = string.Format("Playing: {0}, Recycling: {1}, Watching: {2}",
			                  p, ghosts, spectators);
		} ui.text = n;
	}

	void EndMatch(){ RPC("EnterIdle"); }

	// ------------------------------------------------------------------------

	[RPC] void Enter(){
		state = State.ENTERING;
	}

	[RPC] void StateUpdate(int s){
		print("GOT STATE UPDATE: "+s+" - "+ (State)s);
		state = (State)s;
	}

	[RPC] void EnterSpectate(){
		state = State.SPECTATE;
		Steer(true, "Start spectating");
	}

	[RPC] void EnterMatch(){
		state = State.MATCHING;
		Steer(false, "Enter match");
		if(stabilizer)stabilizer.enabled = true;
		this.Get<HP>().Reset();
		if(proxy.IsMine) Camera.main.gameObject.AddComponent<FogSplash>();
		this.transform.position = Arena.GenPos();
	}

	[RPC] void EnterIdle(){
		Steer(true, "Enter idle mode");
		state = State.IDLE;
	}

	[RPC] void Lose(){
		Steer(true, "Enter ghost mode");
		state = State.GHOST;
	}

	[RPC] void Win(string name) {
		playerName = name;
		state = State.WIN; Invoke("EndMatch", 3);
	}

	[RPC] void NameTag(string name) {
		playerName = name;
		GetComponentInChildren<NameTag>().Set(name);
	}

	void Steer(bool flag, string reason){
		print("Enable steer: "+flag+" - "+reason);
		if(steer) steer.enabled = flag;
		if(stabilizer) stabilizer.enabled = !flag;
	}

	// ------------------------------------------------------------------------

	int playersLeft{ get{
		int n=0;
		foreach(var x in all){ if(x.state==State.MATCHING) n++; }
		return n;
	}}

	int ghosts{ get{
		int n=0;
		foreach(var x in all){ if(x.state==State.GHOST) n++; }
		return n;
	}}

	int playersUnknown{ get{
		int n=0;
		foreach(var x in all){ if(x.state==State.UNKNOWN) n++; }
		return n;
	}}

	int spectators{ get{
		int n=0;
		foreach(var x in all){ if(x.state==State.SPECTATE) n++; }
		return n;
	}}

	int usersEntering{ get{
		int n=0;
		foreach(var x in all){ if(x.state==State.ENTERING) n++; }
		return n;
	}}

	int idleUsers { get{
		int n=0;
		foreach(var x in all){ if(x.state==State.IDLE) n++; }
		return n;
	}}

	int         userCount { get{ return all.Length;     				}}
	bool        matchOver { get{ return playersLeft==0; 				}}
	bool        KO        { get{ return this.Get<HP>().value<=0;        }}

	void OnSpectate(){
		spectateNotice.transform.localScale = Vector3.one;
		Invoke("StopSpectNote", 5);
	}

	void StopSpectNote(){
		spectateNotice.transform.localScale = Vector3.zero;
	}

	GameObject spectateNotice{
		get{ return GameObject.Find("Spectator Notice"); }
	}

	PhotonView  proxy     { get{ return PhotonView.Get(this); 	 		}}
	UserState[] all       { get{ return FindObjectsOfType<UserState>(); }}
	A1.Steer steer        { get{ return this.Get<A1.Steer>(); }}
	Stabilizer stabilizer { get{ return this.Get<Stabilizer>(); }}

	public static string winner{get{
			var players = FindObjectsOfType<UserState>();
			foreach(var k in players){
				if(k.state==State.WIN){
					return k.playerName;
				}
			}
			return null;
	}}

}
