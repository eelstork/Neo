using UnityEngine;

/**
 * DEPRECATED - only keeping this because Rotor relies on it
 * Base class for Rotor and motor.
 * Note: useful defaults can't be provided here; instead, child classes
 * provide their own
 */
public class Governor : MonoBehaviour {

	public Vector3 target;
	public float speed;           // target speed (units/s)
	public float traction;        // motor response (N/kg/units/s)
	public float strength;        // max applicable force (N/kg/s)
	public float minOutput = 12;  // minimum applied output (N/kg)
	[Header("Informative")]
	public float demand;
	public float maxDemand;
	[HideInInspector]
	protected Vector3 @out;

	protected Vector3 Clamp(Vector3 Δ){
		demand = (Δ * traction).magnitude/strength;
		maxDemand = Mathf.Max(demand, maxDemand);
		if(demand>1) Δ /= demand;
		return Δ;
	}

	protected float Δt       { get{ return Time.fixedDeltaTime;       } }
	protected Rigidbody body { get{ return GetComponent<Rigidbody>(); } }
	protected Transform T    { get{ return transform;                 } }

	public float output{ get{ return @out.magnitude; }}

}
