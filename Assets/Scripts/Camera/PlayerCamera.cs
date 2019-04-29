using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCamera : MonoBehaviour {

	public Transform target;

	void Update () {
		if(!target && Connection.localPlayer){
			target = Connection.localPlayer.transform;
		}
		if(target) UpdateCamera();
	}

	protected abstract void UpdateCamera();

}
