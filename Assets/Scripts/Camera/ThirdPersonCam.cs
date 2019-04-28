using UnityEngine;

public class ThirdPersonCam : MonoBehaviour {

	public Transform target;
	public Vector3 offset = new Vector3(0, 2, 8);
	public float height = 1.5f;
	Vector3 origin;
	Quaternion rotation;
	bool local = false;

	void Update(){
		if(!target && Connection.localPlayer){
			target = Connection.localPlayer.transform;
		}
		if(target) UpdateCamera();
	}

	void UpdateCamera(){
		transform.position = target.position+offset;
		Vector3 P = target.position + Vector3.up*height;
		transform.forward = P-transform.position;
	}

}
