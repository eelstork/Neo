using UnityEngine;

public class LookAtCamera : MonoBehaviour {

	public Transform target;

	void Update () { transform.LookAt(target); }
	
}
