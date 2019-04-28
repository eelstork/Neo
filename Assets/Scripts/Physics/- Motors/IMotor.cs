using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMotor {

	void Target(Transform t);
	void Target(Vector3 p);
	void Stop();

}
