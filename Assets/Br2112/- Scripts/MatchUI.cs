using UnityEngine;
using UnityEngine.UI;

public class MatchUI : MonoBehaviour {

	public GameObject ui;
	public Text text;

	void Update () {
		var s = UserState.winner;
		if(s==null){
			ui.SetActive(false);
		}else{
			ui.SetActive(true);
			text.text = "Winner: "+s;
		}
	}

}
