using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HP : MonoBehaviour {

	public int value = 100;

	public void Reset(){ value = 100; }

	void Update(){
		if(value<=0)this.Get<UserState>().RPC("Lose");
	}

	void OnCollisionEnter(Collision c){
		if(!PhotonView.Get(this).IsMine) return;
		var reason = c.collider.gameObject.name;
		print("Collided with "+reason);
		if(reason.StartsWith("Ammo")){
			print("Subtract HP");
			value -= 25;
		}
	}

}
