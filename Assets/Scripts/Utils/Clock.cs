using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {
	
	public int seconds;
	
	void Update (){
		seconds = (int)Time.timeSinceLevelLoad;
	}
	
}
