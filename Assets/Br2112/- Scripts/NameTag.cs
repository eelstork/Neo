using UnityEngine;
using UnityEngine.UI;

public class NameTag : MonoBehaviour {

	public void Set(string s){
		if(s.Length>3){ s = s.Substring(0,2)+"."; }
		this.Get<Text>().text = s;
	}

}
