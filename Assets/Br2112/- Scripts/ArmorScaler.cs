using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// rest camera to dist 20 height 1.5
public class ArmorScaler : MonoBehaviour {

	public float t = 0.1f;

	void Update () {
		//if(GetComponentInParent<PhotonView>().IsMine) return;
		var state = GetComponentInParent<UserState>().state;
		var w = 1f;
		if(state==UserState.State.IDLE
		|| state==UserState.State.SPECTATE || state==UserState.State.GHOST){
			w = 0.1f;
		}
		t = t*0.9f + w*0.1f;
		var factor = Mathf.Lerp(0.1f, 1f, t);
		transform.localScale = Vector3.one*factor;
	}

}
