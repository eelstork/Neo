using UnityEngine;

public class MotorSystem : MonoBehaviour {

	// Even when at rest usually you still want to be looking at the
	// target
	public bool stopRotorOnStop = false;

	public void Target(Target target, bool lookAt=true){
		motor.Target(target);
		if(lookAt) rotor.Target(target);
	}

	public void LookAt(Target target){ rotor.Target(target); }

	public void Stop(){
		motor.Stop();
		if(stopRotorOnStop) rotor.Stop();
	}

	protected IRotor rotor{ get{
		var c = GetComponent<IRotor>(); return c!=null ? c : this.Req<Rotor>();
	}}

	protected IMotor motor{ get{
		var c = GetComponent<IMotor>();
		return c!=null ? c : this.Req<FixedMotor>();
	}}

}
