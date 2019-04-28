using UnityEngine;
using Photon.Realtime; using Photon.Pun; using Net = Photon.Pun.PhotonNetwork;

public class Weapon : MonoBehaviour {

	public float force = 10;
	public string prefab;

	public void Shoot(){
		Vector3 P = GetComponentInChildren<Grasp>().transform.position;
		var x = Net.Instantiate(prefab, P,
					 Quaternion.AngleAxis(Random.Range(0,360), Vector3.up), 0);
		x.name = "Bullet";
		x.transform.Get<Rigidbody>().AddForce(
			force*transform.forward, ForceMode.Impulse);
		PhotonView.Get(x).OwnershipTransfer = OwnershipOption.Takeover;
	}

}
