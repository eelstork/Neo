using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

	void Update () {
		if(!Connection.localPlayer) return;
		int value = Connection.localPlayer.GetComponent<HP>().value;
		GetComponentInChildren<Text>().text = "HP: " + value;
	}

}
