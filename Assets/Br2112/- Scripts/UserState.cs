﻿using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using RPC = Photon.Pun.PunRPC;

public class UserState: MonoBehaviour {

	public enum State{ ENTERING, SPECTATE, IDLE, MATCHING, WIN, GHOST }
	public float spawnRadius = 20;
	public int minUsersToMatch = 3;
	public State state = State.ENTERING;
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
		if(playersLeft>0) RPC("EnterSpectate");
		else RPC("EnterIdle");
		InvokeRepeating("DoUpdate", 1, 1);
	}

	void Update(){
		_playersLeft = playersLeft;
	}

	void DoUpdate(){
		if(!proxy.IsMine) return;
		UpdateUI();
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

	void Steer(bool flag, string reason){
		print("Enable steer: "+flag+" - "+reason);
		steer.enabled = flag;
		stabilizer.enabled = !flag;
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

	int spectators{ get{
		int n=0;
		foreach(var x in all){ if(x.state==State.SPECTATE) n++; }
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
