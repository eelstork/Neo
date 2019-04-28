using UnityEngine;

public class Rotor : Governor, IRotor {

	public bool planar = true;

	void IRotor.Target(Vector3 p){ target = p; enabled = true; }
	void IRotor.Stop(){ enabled = false; }

	void Awake(){
		if(speed==0)    speed    = 10;
		if(traction==0) traction = 20;
		if(strength==0) strength = 50;
	}

	void FixedUpdate(){
		var Δ = Vector3.Cross(T.forward, targetDirection)*speed
		        - body.angularVelocity;
	    // TODO: multiply by Δt is wrong here.
		body.AddTorque(Clamp(Δ)*body.mass*traction);
	}

	Vector3 targetDirection{
		get{
			Vector3 u = T.Dir(target);
			if(!planar) return u;
			var v = T.InverseTransformDirection(u);
			v.y = 0;
			return T.TransformDirection(v);
		}
	}

}
