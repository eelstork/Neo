using UnityEngine;

public class Respawn : MonoBehaviour {

	void Update () {
		if(transform.position.y < -10){
			transform.position = Vector3.up * 10;
			this.Get<Rigidbody>().velocity = Vector3.zero;
			this.Get<UserState>().RPC("Lose");
		}
	}

}
