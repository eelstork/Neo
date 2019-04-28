using UnityEngine;
using N = Notifications;
namespace A1{ public class Push : Ability {

	public float force = 10;
	Transform target;

	public void Invoke(Transform x){
		target = Req(x);
		enabled = true;
	}

	void Update(){
		ReachAction(target, () => {
			body.AddForce(transform.Dir(target)*force, ForceMode.Impulse);
			Notify(N.OnPush, target);
			target = null;
			ExecPostAction();
		});
	}

}}
