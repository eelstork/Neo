using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC : MonoBehaviour {

	public bool Submit(string[] words){
		if(!canUseCommands){
			// false so that chat messages appear
			if(canUseChat) return false;
			// otherwise true to filter out chat
			else return true;
		}
		print(string.Join("-", words));
		string cmd = words[0];
		if(cmd=="move") return Move(words[1]);
		if(cmd=="sneak") return Sneak(words[1]);
		if(cmd=="strafe") return Strafe(words[1]);
		if(cmd=="rotate") return Rotate(words[1]);
		if(cmd=="nudge") return Nudge(words[1]);
		if(cmd=="shoot") return Shoot();
		if(cmd=="jump") return Jump();
		if(cmd=="dig") return Dig();
		if(cmd=="respawn") return Respawn();
		return false;
		//return this.Get<A1.ActorController2>().Command(cmd);
	}

	bool Move(string x){
		print("Move");
		if(x=="forward") precise.Move(T.forward);
		if(x=="back") precise.Move(-T.forward);
		Cost(2);
		return true;
	}

	bool Sneak(string x){
		print("Move");
		if(x=="forward") precise.Move(T.forward/2);
		if(x=="back") precise.Move(-T.forward/2);
		Cost(1);
		return true;
	}

	bool Rotate(string x){
		print("Rotate");
		if(x=="left") precise.Rotate(-T.right);
		if(x=="right") precise.Rotate(T.right);
		Cost(1);
		return true;
	}

	bool Nudge(string x){
		print("Nudge");
		var angle = int.Parse(x);
		precise.RotateByAngle(angle);
		Cost(angle);
		return true;
	}

	bool Strafe(string x){
		print("Strafe");
		if(x=="left") precise.Move(-T.right/2);
		if(x=="right") precise.Move(T.right/2);
		Cost(1);
		return true;
	}

	bool Jump(){
		print("Jump"); precise.Move(T.up); Cost(5); return true;
	}

	bool Respawn(){
		print("Respawn"); transform.position = Arena.GenPos();
		Halve();
		return true;
	}

	bool Dig(){
		print("Dig"); precise.Move(-T.up); Cost(1); return true; }

	bool Shoot(){
		weapon.Shoot();
		Cost(1);
		return true;
	}

	bool canUseChat{get{
		UserState.State state = this.Get<UserState>().state;
		bool allowed = false;
		switch(state){
			case UserState.State.MATCHING:
				allowed=true; break;
			case UserState.State.SPECTATE:
				allowed=true; break;
		}
		return allowed;
	}}

	bool canUseCommands{get{
		UserState.State state = this.Get<UserState>().state;
		bool allowed = false;
		switch(state){
			case UserState.State.IDLE:
				allowed = true;
				break;
			case UserState.State.MATCHING:
				allowed=true;
				break;
		}
		return allowed;
	}}

	void Cost(int n){ this.Get<HP>().Pay(n); }

	void Halve(){ this.Get<HP>().Div(2); }

	Weapon weapon{get{ return this.Req<Weapon>(); }}
	Precise precise{get{ return this.Req<Precise>(); }}
	Transform T{get{ return transform; }}
	Rigidbody body{ get{ return this.Req<Rigidbody>(); }}

}
