using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; using RPC = Photon.Pun.PunRPC;

public class Ownership : MonoBehaviourPunCallbacks{

	public float delay = 5;
	[Header("Informative")]
	public List<PhotonView> owned;


	override public void OnJoinedRoom(){ InvokeRepeating("Own", delay, delay); }

	void Own(){
		var views = FindObjectsOfType<PhotonView>();
		foreach (PhotonView view in views) {
			if (view == self) continue;
			var other = OwningAvatar(view);
			bool ownView;
			// unowned views may arise during 'unprefabbed' testing.
			if(!other) ownView = true; else{
				float d0 = this.Dist(view), d1 = other.Dist(view);
				ownView = (d0<d1);
			} if(ownView) self.Own(view.ViewID);
		} ListOwned();
	}

	void ListOwned(){
		owned = new List<PhotonView>();
		var views = FindObjectsOfType<PhotonView>();
		foreach (PhotonView view in views) {
			if (view.Owner == self.Owner) owned.Add(view);
		}
	}

	Transform OwningAvatar(PhotonView view){
		Avatar[] avatars = FindObjectsOfType<Avatar>();
		foreach (var v in avatars) {
			if (v.GetComponent<PhotonView>().Owner == view.Owner) {
				return v.transform;
			}
		}
		return null;
	}

	PhotonView self{ get{ return this.Get<PhotonView>(); } }

}
