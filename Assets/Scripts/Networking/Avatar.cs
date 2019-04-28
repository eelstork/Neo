using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tagging behavior for player avatars
public class Avatar : MonoBehaviour {

	public static List<Avatar> all = new List<Avatar>();

	void Start(){ all.Add(this); }

}
