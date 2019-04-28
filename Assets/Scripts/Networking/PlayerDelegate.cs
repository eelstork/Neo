using UnityEngine;
using Photon.Pun;

public abstract class PlayerDelegate : MonoBehaviourPunCallbacks{

	abstract public void OnAvatarCreated(GameObject player);

}
