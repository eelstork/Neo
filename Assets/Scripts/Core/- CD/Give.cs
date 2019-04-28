using UnityEngine;
using Photon.Pun; using RPC = Photon.Pun.PunRPC;
using N = Notifications;

namespace A1{ public class Give : Ability {

	Transform gift; Transform receiver;

	public void Invoke(Transform receiver, Transform obj){
		gift = Req(obj);
		if(!Controls(obj)) throw new System.Exception(string.Format(
			"{0} does not control {1}", gameObject.name, obj.gameObject.name));
		this.receiver = Req(receiver);
		enabled = true;
	}

	bool Controls(Transform t){
		while(t!=null){
			t=t.parent;
			if(t==transform) return true;
		} return false;
	}

	void Update(){ ReachRPC(receiver, "DoGive", gift.Id(), receiver.Id()); }

	[RPC] public void DoGive(int id, int dstId){
		gift = id.Transform();
		receiver = dstId.Transform();
		gift.SetParent(receiver);
		gift.localPosition = Vector3.forward;
		receiver.GetComponent<PhotonView>().Own(id);
		Notify(N.OnGive, gift);
		gift = receiver = null;
	}

}}
