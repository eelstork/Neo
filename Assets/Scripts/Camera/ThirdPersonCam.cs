using UnityEngine;

public class ThirdPersonCam : PlayerCamera {

	public Vector3 offset = new Vector3(0, 2, 8);
	public float minDist = 2;
	public float maxDist = 50;

	public bool RotateX(float angle){
		if(!target) return false;
		var p = target.position;
		transform.RotateAround(target.position, transform.right, angle);
		var u = transform.position-target.position;
		if(u.y<0) return false;
		offset = u;
		return true;
	}

	public void RotateY(float angle){
		if(!target) return;
		var p = target.position;
		transform.RotateAround(target.position, Vector3.up, angle);
		offset = transform.position-target.position;
	}

	public void Zoom(float s){
		if(s==0) return;
		offset*=s;
		if(offset.magnitude>maxDist) offset = offset.normalized*maxDist;
		if(offset.magnitude<minDist) offset = offset.normalized*minDist;
	}

	override protected void UpdateCamera(){
		transform.position = target.position + offset;
		transform.LookAt(target);
	}

}
