using System;
using UnityEngine;
using Photon.Pun;

namespace A1{
[RequireComponent(typeof(PhotonView))]
public class ActorController2 : MonoBehaviour {

	[Header("Info")]
	public string message;

	// look-at, push, reach, tell and gesture not used here
	void Update(){
		if(dm.Will("Transfer"))
			Try(() => this.Req<Give>().Invoke(cx.hand,cx.other));
		if(dm.Will("Ingest")) Try(() => this.Req<Ingest>().Invoke(cx.that));
		if(dm.Will("Expel"))  Try(() => this.Req<Expel>().Invoke(cx.content));
		if(dm.Will("Grab"))   Try(() => this.Req<Grab>().Invoke(cx.that));
		if(dm.Will("Jump"))   Try(() => this.Req<Jump>().Invoke(Vector3.up));
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
