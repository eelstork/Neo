using UnityEngine;
using Photon.Pun; using RPC = Photon.Pun.PunRPC;
using N = Notifications;

namespace A1{ public class Expel : Ability {

	[Header("Metabolism")]
	[Range(0f, 1f)] public float conversion = 0.2f;
	public string output;
	[Header("Cosmetic")]
	public Color taint = Color.black;
	[Range(0f, 1f)] public float tainting = 0.35f;

	public void Invoke(Transform x){
		enabled = true;
		RPC("DoExpel", Req(x).Id());
	}

	[RPC] public void DoExpel(int id){
		var child = id.Transform();
		var outlets = GetComponentsInChildren<Outlet>();
		switch(outlets.Length){
		  case 0: child.localPosition = Vector3.back;                    break;
		  case 1: child.position = outlets[0].transform.position;        break;
		  default: Debug.LogWarning("Mult. outlets " + gameObject.name); break;
		}
		if(output.Length>0) child.gameObject.name = output;
		child.SetParent(null);
		child.gameObject.SetActive(true);
		Enabler.Enable(child);
		child.Taint(taint, tainting);
		this.GainMass(child, conversion);
		Notify(N.OnExpel, child);
	}

}}
