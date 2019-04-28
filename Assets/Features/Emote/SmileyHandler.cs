using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SmileyHandler : MonoBehaviour{

	public Text text;
		
	public void OnTell(string message){
		// always processing smileys ensures that previous smiley will show
		// on skin change, if the current skin allows it
		var alpha = message.Any(char.IsLetterOrDigit);
		if (!alpha) text.text = message;
	}

	// Hide/show canvas depending on current skin
	public void OnSkinChanged(){
		transform.Find("Face").Get<Canvas>().enabled = supported;
	}

	bool supported{ get{ return (!props) || props.enableSmileys; }}

	SkinProperties props{ get{ return this.Get<SkinProperties>(); }}

}
