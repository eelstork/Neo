using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BrainState : MonoBehaviour {

	void Update () {
		var state = GetComponentInParent<UserState>().state;
		bool show = true;
		if(state==UserState.State.GHOST || state==UserState.State.MATCHING){
			show = false;
		}
		this.Get<Renderer>().enabled = show;
	}

}
