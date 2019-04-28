using UnityEngine;

public class Velocity : MonoBehaviour{

	public Vector3 velocity;
	public float speed;
	public float rotationalSpeed;
	public float maxRotationalSpeed;
	public float rotationalSpeedCap = 120;
	Vector3 P, u;
	
	void Update (){
		var Q = transform.position; 
		var v = transform.forward; 
		var Δ = Time.deltaTime;
		velocity = (Q - P) / Δ;
		speed = velocity.magnitude;
		rotationalSpeed = Vector3.Angle(u, v);
		maxRotationalSpeed = Mathf.Max(rotationalSpeed, maxRotationalSpeed);
		if (rotationalSpeedCap>0 && rotationalSpeed > rotationalSpeedCap) {
			Debug.LogError(this.gameObject.name + " exceeded max r/f: " 
			                                    + rotationalSpeed);
		}

		u = v; P = Q;
	}
	
}
