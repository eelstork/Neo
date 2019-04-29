using UnityEngine;

public class RadialCam : PlayerCamera {

	public float angle = 15;
	public float distance = 10;
	public float height = 1.5f;

	override protected void UpdateCamera(){
		var d = Mathf.Cos(angle*Mathf.Deg2Rad)*distance;
		var h = Mathf.Sin(angle*Mathf.Deg2Rad)*distance;
		var u = target.position; u.y = 0;
		u = u.normalized*d+Vector3.up*h;
		transform.position = target.position+u;
		transform.forward = this.Dir(target.position + Vector3.up*height);
	}

}
