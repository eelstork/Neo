using UnityEngine;
using System.Collections.Generic;

public class BasicAnimationHandler : MonoBehaviour {

	const string WALK = "walk", IDLE = "idle";
	//
	public float threshold = 0.1f;
	public float speed = 1;
	[Range(0.0f, 0.5f), Tooltip("Blend duration in seconds")]
	public float idleBlend = 0.2f, walkBlend = 0.1f;
	[Header("Informative")]
	public string current;
	//
	float actionEndTime = 0;

	public float Play(string action, float blend = 0.2f){
		var state = Map(action);
		if(state!=null) {
			current = string.Format("{0} (for {1})", state.name, action);
			state.speed = speed;
			float duration = state.length/speed;
			driver.Blend(state.name, 1f, blend);
			actionEndTime = Time.time + duration;
			return duration;
		}else{
			print("No anim for action: "+action);
			return 0;
		}
	}

	void Update(){
		if(Time.time < actionEndTime) return;
		current = isWalking ? Map(WALK).name : Map(IDLE).name;
		driver[current].speed = speed;
		driver.Blend(current, 1f, isWalking ? walkBlend : idleBlend);
	}

	AnimationState Map(string state){
		if(driver[state]) return driver[state];
		if(AnimationData.map.ContainsKey(state))
			foreach(string x in AnimationData.map[state])
				if(driver[x]) return driver[x];
		return null;
	}

	bool isWalking   { get{ return body.velocity.magnitude>threshold;  } }
	Animation driver { get{ return this.Get<Animation>(); }}
	Rigidbody body   { get{ return this.Req<Rigidbody>(); }}

}
