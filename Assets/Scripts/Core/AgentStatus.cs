using UnityEngine;
namespace A1{
public class AgentStatus : MonoBehaviour {

	const string PENDING = "{0} - {1:0.##}m and {2:0.#}° from {3}";

	public string status;

	public void Pending(Ability current, Transform target){
	    status = string.Format(PENDING,
		                       current.GetType().Name,
							   this.Dist(target), this.Angle(target), target);
	}

	public void Pending(Ability current, Target target){
		status = string.Format(PENDING,
							   current.GetType().Name,
							   this.Dist(target), this.Angle(target), target);
	}

	public void Clear(){ status = null; }

}
}
