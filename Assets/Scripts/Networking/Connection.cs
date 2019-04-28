using UnityEngine;
using Photon.Realtime; using Photon.Pun; using Net = Photon.Pun.PhotonNetwork;

public class Connection : MonoBehaviourPunCallbacks {

	public enum Status {Offline, Connecting, Joining, Creating, Rooming};
	//
	public string room    = "The Room";
	public bool   verbose = true;
	public static GameObject localPlayer = null;
	[Header("Informative")]
	public Status status = Status.Offline;

	void Start(){
		PlayerPrefs.SetString("Username", "Sup");
		status = Status.Connecting;
		Net.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster(){
		status = Status.Joining;
		Net.JoinRandomRoom();
	}

	public override void OnJoinRandomFailed(short returnCode, string message){
		status = Status.Creating;
		var options = new RoomOptions();
		options.IsVisible = true;
		options.IsOpen = true;
		options.MaxPlayers = 20;
		Net.CreateRoom(room, roomOptions: options);
	}

	public override void OnCreatedRoom(){}

	public override void OnJoinedRoom(){
		status = Status.Rooming;
		room = Net.CurrentRoom.Name;
		Log("Joined room: " + Net.CurrentRoom.Name);
		localPlayer = this.Get<Instantiator>().Invoke();
		var p = GameObject.Find("Player");
		if(p) p.SendMessage("OnAvatarCreated", localPlayer);
	}

	override public void OnDisconnected(DisconnectCause cause){
		if(cause!=DisconnectCause.DisconnectByClientLogic)
			Err("Photon error: " + cause);
	}

    override public string ToString(){
		var rm     = Net.CurrentRoom.Name;
		var player = Net.LocalPlayer.UserId;
		var c      = Net.CountOfPlayers;
		var m      = Net.IsMasterClient;
		var r      = Net.CloudRegion;
	    return string.Format("{0} [{1}]; p = {2}; master? {3}; region: {4}",
							 "Inside", rm, c, m ? "true" : "false", r);
    }

	void Log(string x){ if(verbose) Debug.Log(x); }
	void Err(string x){ Debug.LogWarning(x);      }

}
