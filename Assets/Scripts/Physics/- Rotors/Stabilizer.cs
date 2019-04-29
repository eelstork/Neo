using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabilizer : MonoBehaviour {

	public float scalar = 500;
	public float strength = 20;
	public float limit = 45;
	[Header("Debug")]
	public float output;

	void FixedUpdate(){
		var u = up;
		var angle = Vector3.Angle(u, transform.up);
		if(angle<limit) return;
		//Debug.DrawRay(transform.position, u, Color.cyan);
		var Δ = Vector3.Cross(transform.up, u);
		var F = Δ*scalar;
		if(F.magnitude>strength) F*=strength/F.magnitude;
		body.AddTorque(F*body.mass);
		output = F.magnitude;
	}

	Vector3 up{ get{
		return Vector3.up;
	}}

	float metric{ get{ return collider.bounds.size.y; }}

	Rigidbody body{ get{ return this.Req<Rigidbody>(); } }
	new Collider collider{ get{ return this.Get<Collider>(); }}

}
