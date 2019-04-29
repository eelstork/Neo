using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HP : MonoBehaviour {

	const  int DEFAULT = 100;
	const  int MAX     = 500;
	public int value   = 100;

	void Awake(){ value = DEFAULT; }

	public void Reset()     { value =  DEFAULT; }
	public void Pay(int x)  { value -= x;  }
	public void Div(int n)  { value /= n;    }
	public void Grant(int n){ value += n; if(value>MAX) value=MAX; }

	void Update(){
		if(value<=0)this.Get<UserState>().RPC("Lose");
	}

	void OnCollisionEnter(Collision c){
		if(!PhotonView.Get(this).IsMine) return;
		var reason = c.collider.gameObject.name;
		if(reason.StartsWith("Ammo")){
			//rint("Subtract HP");
			value -= 25;
		}
	}

}
