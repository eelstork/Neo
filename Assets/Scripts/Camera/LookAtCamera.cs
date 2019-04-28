using UnityEngine;

public class LookAtCamera : MonoBehaviour {

	public Transform target;

	void Update () {
		if(!target && Connection.localPlayer){
			target = Connection.localPlayer.transform;
		}
		if(target) transform.LookAt(target);
	}

}
