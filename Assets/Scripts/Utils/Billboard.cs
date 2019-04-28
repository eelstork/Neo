using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour {

	public float secondsPerLetter = 0.3f;
	public float baseTime  = 1f;
	public float minHeight = 1.65f;
	public float maxHeight = 5.00f;
	public float minScale  = 0.65f;
	public float maxScale  = 2.50f;
	[Header("Debug")]
	public bool deleteAfterWait = true;
	public string dist;
	//
	float height = 5f;
	float stamp = 0;
	float scale = 1f;

	void Start(){
		canvas.enabled = false;
		transform.localScale = Vector3.zero;
	}

	public void Display(string message){
		var text = GetComponentInChildren<Text>();
		text.text = message;
		canvas.enabled = true;
		stamp = Time.time + baseTime + secondsPerLetter*message.Length;
	}

	void Update () {
		dist = string.Format("{0:0.#}m", this.Dist(Camera.main));
		UpdateParams(this.Dist(Camera.main));
		if(Time.time<stamp || !deleteAfterWait){
			transform.position   = transform.parent.position+Vector3.up*height;
			transform.forward    = Camera.main.transform.forward;
			transform.localScale = Vector3.one * scale * 0.01f;
		}else{
			canvas.enabled = false;
		}
	}

	void UpdateParams(float d){
		if(d<4)d=4; if(d>25)d=25;
		float t = (d-4)/(25-4);
		height = Mathf.Lerp(minHeight, maxHeight, t);
		scale  = Mathf.Lerp(minScale,  maxScale, t);
	}

	Canvas canvas {get{ return GetComponentInChildren<Canvas>(); }}

}
