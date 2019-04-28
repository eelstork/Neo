using UnityEngine;

public class ScalarMotor : AbstractMotor  {

	public float force = 10f;

	override protected Vector3 EvalForce(bool active){
		return active ? this.Dir(target)*this.Dist(target)*force
		              : Vector3.zero;
	}

}
