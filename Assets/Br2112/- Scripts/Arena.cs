using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour {

	public float radius = 10;
	public float altitude = 10;

	public static Vector3 GenPos(){
		Arena instance = FindObjectOfType<Arena>();
		var P = Random.insideUnitCircle*instance.radius;
		return new Vector3(P.x, instance.altitude, P.y);
	}

	void OnDrawGizmosSelected(){
		Vector3 P = Vector3.up*altitude;
		Vector3 l = Vector3.left*radius;
		Vector3 f = Vector3.forward*radius;
		for(float i=0; i<Mathf.PI*2; i+=Mathf.PI/16f){
				float j = i+Mathf.PI/16f;
				Debug.DrawLine(P + l*Mathf.Cos(i) + f*Mathf.Sin(i),
							   P + l*Mathf.Cos(j) + f*Mathf.Sin(j));
		}
	}

}
