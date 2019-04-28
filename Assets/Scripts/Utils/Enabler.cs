using UnityEngine;

public class Enabler : MonoBehaviour {

	public static void Disable (Transform that) {
		// Disable<Actor>(that);
		// Disable<Motor>(that);
		Disable<Rotor>(that);
		var body = that.Get<Rigidbody>();
		if(body) body.isKinematic = true;
		var c = that.Get<Collider>();
		if(c) c.enabled = false;
	}

	public static void Enable (Transform that) {
		// Enable<Actor>(that);
		// Enable<Motor>(that);
		Enable<Rotor>(that);
		var body = that.Get<Rigidbody>();
		if(body) body.isKinematic = false;
		var c = that.Get<Collider>();
		if(c) c.enabled = true;
	}

	static void Disable<T>(Transform that) where T: MonoBehaviour{
		var c = that.Get<T>();
		if(!c) return;
		c.enabled = false;
	}

	static void Enable<T>(Transform that) where T: MonoBehaviour{
		var c = that.Get<T>();
		if(!c) return;
		c.enabled = true;
	}

}
