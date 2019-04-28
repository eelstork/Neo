using UnityEngine;
using Photon.Pun; using RPC = Photon.Pun.PunRPC;
using N = Notifications;

namespace A1{ public class Gesture : Ability {

	string anim; Transform target;

	public void Invoke(string anim){
		Invoke(anim, null);
	}

	public void Invoke(string anim, Transform target){
		this.anim = Req(anim);
		this.target = target;
		if(!target) RPC("DoGesture", anim);
		enabled = true;
	}

	void Update(){ ReachRPC(target, "DoGesture", anim); }

	[RPC] public void DoGesture(string anim){
		print("(should play anim: "+anim+")");
		Notify(N.OnGesture, anim);
		this.anim = null;
	}

}}
