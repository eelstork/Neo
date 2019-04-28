using UnityEngine;

public abstract class AbstractMotor : MonoBehaviour, IMotor {

	public bool scaleByMass = true;
	[Header("Informative")]
	public float output;
	//
	protected Target target;

	void IMotor.Target(Transform t) { target = t; }
	void IMotor.Target(Vector3 p) { target = p; }
	void IMotor.Stop(){ target = null; }

	void FixedUpdate(){
		var active = target && target.IsValid();
		var F = EvalForce(active);
		if(F.magnitude>0){
			Debug.DrawRay(transform.position, F*0.1f, Color.green);
			if(scaleByMass) F *= body.mass;
			body.AddForce(F);
		}
		output = F.magnitude;
	}

	protected abstract Vector3 EvalForce(bool active);

	protected Rigidbody body{ get{ return this.Req<Rigidbody>(); }}

}
