using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Net = Photon.Pun.PhotonNetwork;
using RPC = Photon.Pun.PunRPC;

public class SkinLoader : PlayerDelegate{

	GameObject skin; // the proxy

	override public void OnAvatarCreated(GameObject avatar){
		var skinName = PlayerPrefs.GetString("Skin", "DefaultSkin");
		ApplySkin(skinName);
	}

	public void ApplySkin(string name){
		PlayerPrefs.SetString("Skin", name);
		PlayerPrefs.Save();
		view.RPC("UpdateSkin", RpcTarget.All, name);
	}

	override public void OnPlayerEnteredRoom(Player player){
		view.RPC("UpdateSkin", player, name);
	}

	PhotonView view{
		get{ return PhotonView.Get(Connection.localPlayer); }
	}

}
