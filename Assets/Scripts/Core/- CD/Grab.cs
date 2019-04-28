using UnityEngine;
using Photon.Pun; using RPC = Photon.Pun.PunRPC;
using N = Notifications;

namespace A1{ public class Grab : Ability {

	Transform target;

	public void Invoke(Transform x){
		target = Req(x);
		enabled = true;
	}

	void Update(){ ReachRPC(target, "DoGrab", target.Id()); }

	[RPC] public void DoGrab(int id){
		target = id.Transform();
		var grasp = GetComponentInChildren<Grasp>();
		target.SetParent(grasp ? grasp.transform : transform);
		Enabler.Disable(target);
		target.localPosition = grasp ? Vector3.zero : Vector3.forward;
		proxy.Own(id);
		Notify(N.OnGrab, target);
		target = null;
	}

}}
