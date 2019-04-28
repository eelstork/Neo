using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using RPC = Photon.Pun.PunRPC;

public class UserState: MonoBehaviour {

	public enum State{ ENTERING, SPECTATE, IDLE, MATCHING, WIN, GHOST }

	public int minUsersToMatch = 3;
	public State state = State.ENTERING;
	[Header("Debug")]
	public int _playersLeft;

	// ------------------------------------------------------------------------

	public void RPC(string msg){ proxy.RPC(msg, RpcTarget.All); }

	// ------------------------------------------------------------------------

	void Start(){
		if(playersLeft>0) RPC("EnterSpectate");
		else RPC("EnterIdle");
		InvokeRepeating("DoUpdate", 1, 1);
	}

	void Update(){
		_playersLeft = playersLeft;
	}

	void DoUpdate(){
		if(!PhotonView.Get(this).IsMine) return;
		if(winner!=null) return;
		switch(state){
			case State.ENTERING:
				Debug.LogError("Should not be here"); break;
			case State.IDLE:
				if(userCount>=minUsersToMatch) RPC("EnterMatch");
				break;
			case State.SPECTATE:
				if(matchOver) RPC("EnterIdle");
				break;
			case State.MATCHING:
				if(idleUsers>0) return;
				if(KO)  RPC("Lose");
				if(playersLeft==1) {
					print("I am the winner");
					RPC("Win");
				}
				break;
			case State.GHOST:
			 	if(matchOver) RPC("EnterIdle");
				break;
		}
		GetComponentInChildren<Billboard>().Display(state.ToString());
	}

	void EndMatch(){ RPC("EnterIdle"); }

	// ------------------------------------------------------------------------

	[RPC] void EnterSpectate () { state = State.SPECTATE; }
	[RPC] void EnterMatch    () { state = State.MATCHING; }
	[RPC] void EnterIdle	 () { state = State.IDLE; }
	[RPC] void Lose			 () { state = State.GHOST; }
	[RPC] void Win 			 () { state = State.WIN; Invoke("EndMatch", 3); }

	// ------------------------------------------------------------------------

	int playersLeft{ get{
		int n=0;
		foreach(var x in all){ if(x.state==State.MATCHING) n++; }
		return n;
	}}

	int idleUsers { get{
		int n=0;
		foreach(var x in all){ if(x.state==State.IDLE) n++; }
		return n;
	}}

	int         userCount { get{ return all.Length;     				}}
	bool        matchOver { get{ return playersLeft==0; 				}}
	bool        KO        { get{ return false;          				}}
	PhotonView  proxy     { get{ return PhotonView.Get(this); 	 		}}
	UserState[] all       { get{ return FindObjectsOfType<UserState>(); }}

	public static string winner{get{
			var players = FindObjectsOfType<UserState>();
			foreach(var k in players){
				if(k.state==State.WIN){
					return k.ToString();
				}
			}
			return null;
	}}

}
