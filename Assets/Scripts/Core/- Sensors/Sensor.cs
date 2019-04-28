using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sensor : MonoBehaviour {

	public float radius = 3f;
	public bool underwater = false;

	public Transform Any(string name){
		var query = from Transform t in FindObjectsOfType<Transform>()
				    where t.gameObject.name == name
						  && MatchMedium(t.position)
						  && this.Dist(t) < radius
					      && transform.CanSee(t, transform.up) select t;
		var array = query.ToArray();
		if(array.Length==0) return null;
		return array[Random.Range(0, array.Length)];
	}

 	bool MatchMedium(Vector3 P){ return underwater ? P.y<0 : P.y>0; }

}
