using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HP : MonoBehaviour {

	const  int DEFAULT = 100;
	const  int MAX     = 500;
	public int value   = 100;

	void Awake(){ value = DEFAULT; }

	public void Reset()     { value =  DEFAULT; Upd(); }
	public void Pay(int x)  { value -= x; Upd();  }
	public void Div(int n)  { value /= n; Upd();    }
	public void Grant(int n){ value += n; if(value>MAX) value=MAX; Upd(); }

	void Upd(){
		float s = value*0.01f;
		if(s<0.8f) s=0.8f;
		transform.localScale = Vector3.one*s;
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
