using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Net = Photon.Pun.PhotonNetwork;

public class Instantiator : MonoBehaviour {

	public string avatar = "Avatar";
	public bool NPC = false;
	[Header("Spawn")]
	public Vector3 @fixed = Vector3.zero;
	public bool random = true;
	public float radius = 24f;
	public float height = 12f;

	public GameObject Invoke(){
		if(avatar.Length<=0) return null;
		var proxy = GameObject.Find(avatar);
		// If an avatar is active, assume a simple 1P test
		// and stop
		if(proxy){
			print("Proxy already exists so let's run a simple SP test");
			return proxy;
		}
		return NPC ? CreateNPC() : CreatePlayer();
	}

	GameObject CreatePlayer(){
		print("Create a player object");
		return Net.Instantiate(
			avatar, random ? spawn : @fixed, Quaternion.identity, 0);
	}

	GameObject CreateNPC(){
		if(Net.IsMasterClient){
			print(  "We're master client so let's wait for another client\n"
			      + "to instantiate the test NPC");
			return null;
		}
		print("Not master client so let's create a NPC");
		return Net.Instantiate(avatar, @fixed, Quaternion.identity, 0);
	}

	Vector3 spawn{ get{
		var O = Random.insideUnitCircle;
		return new Vector3(O.x*radius, height, O.y*radius);
	}}

}
