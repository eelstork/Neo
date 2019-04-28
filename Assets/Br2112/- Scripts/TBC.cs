using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC : MonoBehaviour {

	public bool Submit(string[] words){
		print(string.Join("-", words));
		string cmd = words[0];
		if(cmd=="move") return Move(words[1]);
		if(cmd=="sneak") return Sneak(words[1]);
		if(cmd=="strafe") return Strafe(words[1]);
		if(cmd=="rotate") return Rotate(words[1]);
		if(cmd=="shoot") return Shoot();
		if(cmd=="jump") return Jump();
		if(cmd=="dig") return Dig();
		return false;
		//return this.Get<A1.ActorController2>().Command(cmd);
	}

	bool Move(string x){
		print("Move");
		if(x=="forward") precise.Move(T.forward);
		if(x=="back") precise.Move(-T.forward);
		return true;
	}

	bool Sneak(string x){
		print("Move");
		if(x=="forward") precise.Move(T.forward/2);
		if(x=="back") precise.Move(-T.forward/2);
		return true;
	}

	bool Rotate(string x){
		print("Rotate");
		if(x=="left") precise.Rotate(-T.right);
		if(x=="right") precise.Rotate(T.right);
		return true;
	}

	bool Strafe(string x){
		print("Strafe");
		if(x=="left") precise.Move(T.right/2);
		if(x=="right") precise.Move(-T.right/2);
		return true;
	}

	bool Jump(){ print("Jump"); precise.Move(T.up); return true; }

	bool Dig(){ print("Dig"); precise.Move(-T.up); return true; }

	bool Shoot(){
		weapon.Shoot();
		return true;
	}

	Weapon weapon{get{ return this.Req<Weapon>(); }}
	Precise precise{get{ return this.Req<Precise>(); }}
	Transform T{get{ return transform; }}
	Rigidbody body{ get{ return this.Req<Rigidbody>(); }}

}
