using UnityEngine;

namespace A1{ public class Jump : Ability {

	public float force = 200;

	public void Invoke(Vector3 direction) {
		Req<Rigidbody>().AddForce(direction*force);
	}

}}
