using UnityEngine;

public class TimeScale : MonoBehaviour {

	public float scale = 1f;

	void Update(){ Time.timeScale = scale; }

}
