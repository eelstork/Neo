using UnityEngine;
using Photon.Pun; using Net = Photon.Pun.PhotonNetwork;

public class DestroyOnFallout : MonoBehaviour {

	public float altitude = -10;

	void Update () {
		if(!PhotonView.Get(this).IsMine) return;
		if(transform.position.y < altitude){
			PhotonNetwork.Destroy(PhotonView.Get(this));
			Destroy(this);
		}
	}
}
