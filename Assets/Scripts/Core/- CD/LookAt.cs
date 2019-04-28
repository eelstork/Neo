using UnityEngine;
using N = Notifications;

namespace A1{ public class LookAt : Ability {

	public float threshold;
	Target target;

	public void Invoke(Target x){
		target = Req(x);
		enabled = true;
	}

	void Update(){
		if(!target) return;
		motor.Target(target);
		if(transform.Angle(target)<=12){
			motor.Stop();
			Notify(N.OnLookAt, target);
			target = null;
			ExecPostAction();
		}
	}

}}
