using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A1{ public class Script : MonoBehaviour{

	public bool cycle = false;
	public float maxDuration = 5f;
	[Multiline(10)] public string info;
	//
	List<Command> commands = new List<Command>();
	int index = -1;

	public void Parse(string script){
		string[] args = script.Split('\n');
		foreach(string s in args) Add(s.Trim());
	}

	public void Add(string cmd){ commands.Add((Command)cmd); }

	void Start(){ Next(); }

	void Next(){
		index++;
		if(index>=commands.Count){
			if(cycle) index=0;
			else{ print("- Script exec complete -"); return; }
		}try{
			commands[index].Exec(gameObject, context, maxDuration, Next);
			UpdateInfo();
		}catch(System.Exception){ Invoke("Next", 0.25f); }
	}

	void UpdateInfo(){
		info = ""; for(int i=0; i<commands.Count; i++){
			if(index==i) info+="> "; else info+=("  ");
			info+=commands[i].ToString()+"\n";
		}
	}

	TypeConverter context{ get{ return this.Req<TypeConverter>(); }}

}}
