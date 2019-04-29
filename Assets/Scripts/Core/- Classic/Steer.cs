using UnityEngine;

namespace A1{ public class Steer : Ability{

	public float force = 50f;

	public void Invoke(Vector3 aim){
		if(!enabled) return;
		var t = Camera.main.transform;
		aim = t.TransformDirection(aim);
		aim.y = 0;
		var m = aim.magnitude;
		aim = aim.normalized*m;
		Debug.DrawLine(transform.position, transform.position + aim*3);
		Req<Rigidbody>().AddForce(aim * force);
	}

}}
