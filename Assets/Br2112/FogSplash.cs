using UnityEngine;

public class FogSplash : MonoBehaviour {

	public float initial = 2f;
	public float final = 0.0025f;
	public float duration = 5;
	public float stamp;

	void Start () {
		RenderSettings.fogDensity = initial;
		stamp = Time.time;
	}

	// Update is called once per frame
	void Update () {
		float t = (Time.time-stamp)/duration; if(t>1) t=1;
		RenderSettings.fogDensity = Mathf.Lerp(initial, final, t);
		if(t==1) Destroy(this);
	}
}
