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

	public void RotateByAngle(float amount){
		var u = transform.forward;
		var q = Quaternion.AngleAxis(amount, transform.up);
		var v = q*u;
		var P = transform.position;
		var Q = P + v * 10;
		Debug.DrawLine(P, P+u*5, Color.white);
		Debug.DrawLine(P, Q, Color.green);
		//ebug.LogError("nudging");
		this.GetComponent<IRotor>().Target(Q);
	}

	Rigidbody body{ get{ return this.Req<Rigidbody>(); }}

}
