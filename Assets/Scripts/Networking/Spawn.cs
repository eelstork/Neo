using UnityEngine;
using Photon.Realtime; using Photon.Pun; using Net = Photon.Pun.PhotonNetwork;

public class Spawn : MonoBehaviourPunCallbacks {

	public string prefab;
	public int count = 10;
	public float radius = 1;
	public float rate = 0.1f;  // Additional spawn per seconds
	bool creator = false;

	public override void OnCreatedRoom(){ creator = true; }

	public override void OnJoinedRoom(){
		if(!creator) return;
		for(int i=0; i<count; i++) Generate();
		if(rate>0) InvokeRepeating("Generate", 1/rate, 1/rate);
	}

	void Generate(){
		string suffix = null;
		Vector3 P = transform.position + Random.insideUnitSphere*radius;
		P.y = transform.position.y;
		var x = Net.Instantiate(prefab, P,
				     Quaternion.AngleAxis(Random.Range(0,360), Vector3.up), 0);
		x.name = prefab + (suffix!=null ? suffix : "");
		PhotonView.Get(x).OwnershipTransfer = OwnershipOption.Takeover;
	}

}
