using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Precise : MonoBehaviour {

	public float force = 10;

	public void Move(Vector3 dir){
		body.AddForce(dir*force, ForceMode.Impulse);
	}

	public void Rotate(Vector3 dir){
		this.GetComponent<IRotor>().Target(transform.position + dir*10);
	}

	Rigidbody body{ get{ return this.Req<Rigidbody>(); }}

}
