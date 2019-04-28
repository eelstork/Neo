using UnityEngine;

public class DecisionModel : MonoBehaviour {

    public bool Will(string action){ return Input.GetButtonDown(action); }

    public Vector3 aim{ get{ return new Vector3(x, 0, z); } }

    float x{ get{ return Input.GetAxis("Horizontal"); } }

    float z{ get{ return Input.GetAxis("Vertical"); } }

}
