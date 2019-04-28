using UnityEngine;

public class SimpleCamera : MonoBehaviour {

	public float offset = -5;
	Vector3 origin;
	Quaternion rotation;
	bool local = false;

	void Start(){
		origin = transform.position; rotation = transform.rotation;
	}

	void Update(){
		if(Input.GetButtonUp("Camera Mode")){
			if(transform.position == origin) MakeLocal();
			else MakeGlobal();
		}
		if(local){
			var P = Connection.localPlayer.transform.position;
			transform.position = P + offset*transform.forward;
		}
	}

	void MakeLocal(){ local = true; }

	void MakeGlobal(){ 
		local = false;
		transform.position = origin; transform.rotation = rotation;
	}

}
