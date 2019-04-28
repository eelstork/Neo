using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A1{ public class EditableScript : MonoBehaviour {

	public bool cycle;
	[Multiline(10)] public string script;

	void Start(){
		var c = this.Req<Script>();
		c.Parse(script);
		c.cycle = cycle;
	}

}}
