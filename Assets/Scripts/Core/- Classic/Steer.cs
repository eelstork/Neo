using UnityEngine;

namespace A1{ public class Steer : Ability{

	public float force = 50f;

	public void Invoke(Vector3 aim){
		if(!enabled) return;
		var t = Camera.main.transform;
		aim = t.TransformDirection(aim);
		Debug.DrawLine(transform.position, transform.position + aim*3);
		Req<Rigidbody>().AddForce(aim * force);
	}

}}
