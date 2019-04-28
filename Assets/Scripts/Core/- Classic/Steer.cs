using UnityEngine;

namespace A1{ public class Steer : Ability{

	public float force = 50f;

	public void Invoke(Vector3 aim){
		Req<Rigidbody>().AddForce(aim * force);
	}

}}
