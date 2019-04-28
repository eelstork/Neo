using UnityEngine;
using RPC = Photon.Pun.PunRPC;

public class SkinEffector : MonoBehaviour{

	const string SKIN = "Skin";
	public GameObject[] skins;

	[RPC] public void UpdateSkin(string name){
		print("Update skin to: "+name);
		var prev = transform.Find(SKIN);
		if(prev) DestroyImmediate(prev.gameObject);
		var p = this[name];
		if(!p){ Debug.LogWarning("Skin not found: " + name); p = skins[0]; }
		GameObject obj = Instantiate(
			p, transform, instantiateInWorldSpace: false) as GameObject;
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localRotation = Quaternion.identity;
		obj.name = SKIN;
		gameObject.SendMessage("OnSkinChanged", obj);
	}

	public GameObject this[string name]{ get{
		foreach (var skin in skins) if (skin.name == name) return skin;
		return null;
	} }

}
