using UnityEngine;
using UnityEngine.UI;

public class MessagesUI : MonoBehaviour{

	static MessagesUI instance;

	void Start(){
		instance = this;
	}

	public static void OnMessage(string src, string s, bool didHear,
								 bool clearly){
		if(!didHear) return;
		var msg = src + ": " + (clearly ? s : "(...)");
		if(instance) instance.GetComponent<Text>().text = msg;
	}

}
