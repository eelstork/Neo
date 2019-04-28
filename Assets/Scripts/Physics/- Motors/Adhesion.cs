using UnityEngine;

public class Adhesion : MonoBehaviour {

	public bool scaleByMass = true;
	public float force = 10f;

	void FixedUpdate(){
		var body = this.Req<Rigidbody>();
		body.AddForce(- force * (scaleByMass ? 1 : body.mass) * transform.up);
	}

}
