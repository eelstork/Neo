using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

	int count = 0;

	void Start () {
		count = Time.frameCount;
		InvokeRepeating("Step", 1, 1);
	}

	void Step(){
		var c = Time.frameCount;
		this.Get<Text>().text = (c-count).ToString() + " FPS";
		count = c;
	}
}
