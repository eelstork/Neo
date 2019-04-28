using UnityEngine;

public class KinematicRotor : MonoBehaviour{

	public Vector3 origin = Vector3.zero;
	public Vector3 axis = Vector3.up;
	public float speed = 10;
	
	void FixedUpdate(){
		transform.RotateAround(origin, axis, speed*Time.fixedDeltaTime);
	}

}
