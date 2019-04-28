using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchLight : MonoBehaviour {

    public float val = 0.5f; 
    public float sineFac = 0.1f;
    public float timeFac = 2f;
    public float maxDelay = 5;
    public float min = 0.3f;
    public float max = 1.2f;

	void Start (){
		Invoke("Glitch", Random.value * maxDelay);
	}
	
	void Update (){
		GetComponent<Light>().intensity =
			val + sineFac * Mathf.Sin(Time.time * timeFac);
	}

	void Glitch(){
		if (Random.value > 0.1f) val = Random.Range(min, max);
		if (Random.value > 0.5f) timeFac = Random.Range(0.02f, 100f);
		if (Random.value > 0.2f) timeFac *= 0.01f;
		if (Random.value > 0.5f) sineFac = Random.value*0.3f;
		Invoke("Glitch", Random.value * maxDelay);
	}

}
