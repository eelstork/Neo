using UnityEngine;
using Photon.Pun; using RPC = Photon.Pun.PunRPC;
using N = Notifications;

namespace A1{ public class Tell : Ability {

	Transform receiver; string message; new string name;

	public void Invoke(string name, string message){
		Debug.Log("Tell.Invoke: "+message);
		RPC("DoTell", name, Req(message), -1);
		enabled = true;
	}

	public void Invoke(string name, string message, Transform receiver){
		this.name = name;
		this.message = Req(message);
		this.receiver = Req(receiver);
		enabled = true;
	}

	void Update(){
		ReachRPC(receiver, "DoTell", name, message, receiver.Id());
	}

	[RPC] public void DoTell(string name, string message, int receiverId){
		receiver = receiverId.Transform();
		if(receiver) print("To " + receiver.gameObject.name + ": " + message);
		else print("Msg: " + message);
		var billboard = GetComponentInChildren<Billboard>();
		if(billboard) billboard.Display(message);
		Notify(N.OnTell, message);
		var others = FindObjectsOfType<Hearing>();
		foreach(Hearing other in others)
			other.Signal(name, message, transform);
		message = null;
		receiver = null;
	}

}}
