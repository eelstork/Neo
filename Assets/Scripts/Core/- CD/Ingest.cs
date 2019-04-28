using UnityEngine;
using Photon.Pun; using RPC = Photon.Pun.PunRPC;
using N = Notifications;

namespace A1{ public class Ingest : Ability {

	public Transform target;

	public void Invoke(Transform x){
		target = Req(x);
		enabled = true;
	}

	void Update(){ ReachRPC(target, "DoIngest", target.Id()); }

	[RPC] public void DoIngest(int id){
		print("do ingest");
		target = id.Transform();
		target.SetParent(transform);
		target.localPosition = Vector3.zero;
		target.gameObject.SetActive(false);
		proxy.Own(id);
		Notify(N.OnIngest, target);
		target = null;
		print("ingested");
	}

}}
