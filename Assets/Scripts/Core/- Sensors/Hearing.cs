using UnityEngine;

public class Hearing : MonoBehaviour {

	public float HearingDistance = 8;
	public float SoundDistance = 16;
	public bool debug = false;

	public void Signal(string name, string message, Transform source){
		var d = this.Dist(source);
		bool didHear = d < SoundDistance;
		bool clearly = d < HearingDistance;
		if(debug){
			var logMsg = didHear ? "{0} heard {1} ({2:#.00}m)"
							   	 : "{0} did not hear {1} ({2:#.00}m)";
			print(string.Format(logMsg, this.Name(), source.Name(), d));
		}
		if(isLocalPlayer)
			MessagesUI.OnMessage(name, message, didHear, clearly);
	}

	bool isLocalPlayer{ get{ return gameObject == Connection.localPlayer; }}

}
