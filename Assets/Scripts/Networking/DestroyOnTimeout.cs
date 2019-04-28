using UnityEngine;
using Photon.Pun; using Net = Photon.Pun.PhotonNetwork;

public class DestroyOnTimeout : MonoBehaviour {

	public float timeout = 2;

	void Start () {
		if(!PhotonView.Get(this).IsMine) return;
		Invoke("Timeout", timeout);
	}

	void Timeout(){ PhotonNetwork.Destroy(PhotonView.Get(this)); }
}
