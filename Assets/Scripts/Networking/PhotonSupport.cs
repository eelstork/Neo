using UnityEngine;
using Photon.Pun;
using Net = Photon.Pun.PhotonNetwork;

public static class PhotonSupport{

	public static int Id(this Transform self){
		if(self==null) return -1;
		if(PhotonView.Get(self)==null) throw new System.Exception(
			self.gameObject+" is not a proxy");
        return PhotonView.Get(self).ViewID;
    }

    public static Transform Transform(this int self){
		var proxy = PhotonView.Find(self);
        return proxy ? proxy.transform : null;
    }

	// TODO: for anything calling this now, Ownership management is going
	// to be based on proximity not hierarchy
	public static void Own(this PhotonView photonView, int thatId){
		if(photonView.IsMine)
			PhotonView.Find(thatId).TransferOwnership(Net.LocalPlayer);
	}

}
