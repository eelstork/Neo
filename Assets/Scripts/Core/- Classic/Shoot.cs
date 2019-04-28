using UnityEngine;
using Net = Photon.Pun.PhotonNetwork;

namespace A1{ public class Shoot : MonoBehaviour{

	public PrimitiveType primitive = PrimitiveType.Sphere;
	public float force = 100;
	public string projectile;

	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			var obj = Net.Instantiate(
				projectile,
				transform.position+transform.forward,
				transform.rotation);
			obj.GetComponent<Rigidbody>().AddForce(transform.forward*force);
		}
	}

} }
