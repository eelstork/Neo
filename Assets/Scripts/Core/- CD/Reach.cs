using UnityEngine;
using N = Notifications;

namespace A1{ public class Reach : Ability {

	public Target target;

	public void Invoke(Target x){
		target = Req(x);
		enabled = true;
	}

	void Update(){
		ReachAction(target, () => {
			Notify(N.OnReach, target); target = null; });
	}

}}
