using UnityEngine;

public class AvianMotor : AbstractMotor  {

	public float force = 10f;
	[Range(0f, 0.8f)]public float bias = 0.5f;
	public float speed = 3f;

	override protected Vector3 EvalForce(bool active){
		Vector3 P = active ? (Vector3)target : Vector3.zero;
		if(active) P += Vector3.up*this.Dist(target, planar:true)*bias;
		var u = active ? this.Dir(P) : Vector3.zero;
		var Δ = u*speed-body.velocity;
		return Δ * force;
	}

}
