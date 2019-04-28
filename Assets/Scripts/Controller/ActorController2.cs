using System;
using UnityEngine;
using Photon.Pun;

namespace A1{
[RequireComponent(typeof(PhotonView))]
public class ActorController2 : MonoBehaviour {

	[Header("Info")]
	public string message;

	public bool Command(string cmd){
		if(cmd=="transfer")
			Try(() => this.Req<Give>().Invoke(cx.hand,cx.other));
		else if(cmd=="ingest") Try(() => this.Req<Ingest>().Invoke(cx.that));
		else if(cmd=="expel")  Try(() => this.Req<Expel>().Invoke(cx.content));
		else if(cmd=="grab")   Try(() => this.Req<Grab>().Invoke(cx.that));
		else return false;
		return true;
	}

	void Try(Action act){
		try{
			Ability.StopAll(this); act();
		}catch(ActionContext.Invalid e){ message=e.Message; }
	}

	void FixedUpdate(){
		var u = dm.aim; if(u!=Vector3.zero){
			Ability.StopAll(this); this.Req<Steer>().Invoke(u);
		}
	}

	DecisionModel dm    { get{ return this.Req<DecisionModel>(); }}
	ActionContext cx    { get{ return this.Req<ActionContext>(); }}

}
}
