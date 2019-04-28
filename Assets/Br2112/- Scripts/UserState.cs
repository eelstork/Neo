using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using RPC = Photon.Pun.PunRPC;

public class UserState: MonoBehaviourPunCallbacks {

	public enum State{ ENTERING, SPECTATE, IDLE, MATCHING, GHOST }

	public int minPlayersToMatch = 3;
	public State state = State.ENTERING;

	//public override void OnJoinedRoom(){ InvokeRepeating("DoUpdate", 1, 1); }
	void Start(){ InvokeRepeating("DoUpdate", 1, 1); }

	void DoUpdate(){
		if(!PhotonView.Get(this).IsMine) return;
		switch(state){
			case State.ENTERING:
				if(playersLeft>0) EnterSpectate();
				else EnterIdle();
				break;
			case State.IDLE:
				if(userCount>=minPlayersToMatch) EnterMatch();
				break;
			case State.SPECTATE:
				if(matchOver) EnterIdle();
				break;
			case State.MATCHING:
				if(KO) Lose();
				if(playersLeft==1) Win();
				break;
			case State.GHOST:
			 	if(matchOver) EnterIdle();
				break;
		}
		GetComponentInChildren<Billboard>().Display(state.ToString());
	}

	int playersLeft{ get{
		int n=0;
		foreach(var x in all){ if(x.state==State.MATCHING) n++; }
		return n;
	}}

	int userCount{get{ return all.Length; }}

	bool matchOver{get{ return playersLeft==0; }}

	bool KO{ get{ return false; } }

	UserState[] all{get{ return FindObjectsOfType<UserState>(); }}

	[RPC] void EnterSpectate(){
		state = State.SPECTATE;
	}

	[RPC] void EnterMatch(){
		state = State.MATCHING;
	}

	[RPC] void EnterIdle(){
		state = State.IDLE;
	}

	[RPC] void Lose(){
		state = State.GHOST;
	}

	[RPC] void Win(){
		state = State.IDLE;
	}

}
