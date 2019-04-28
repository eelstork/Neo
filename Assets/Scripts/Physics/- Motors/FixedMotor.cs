using UnityEngine;

public class FixedMotor : AbstractMotor  {

	public float force = 15;
	[Range(0, 1)] public float strafing = 0.5f;

	override protected Vector3 EvalForce(bool active){
		if(!active) return Vector3.zero;
		return force * (this.Dir(target) * strafing
						+ transform.forward * (1-strafing));
	 }

}
