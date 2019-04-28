using UnityEngine;

public class GovernedMotor : AbstractMotor  {

	public float force = 10f;
	public float speed = 3f;

	override protected Vector3 EvalForce(bool active){
		var u = active ? this.Dir(target) : Vector3.zero;
		return (u*speed-body.velocity)*force;
	}

}
