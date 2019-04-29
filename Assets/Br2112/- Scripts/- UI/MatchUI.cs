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
			if(s==null || s.Length==0)s="UNKNOWN USER";
			text.text = s + " is POPULATION ONE";
		}
	}

}
