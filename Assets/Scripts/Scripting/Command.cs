using UnityEngine; using Action = System.Action;

namespace A1{ public class Command {

	string[] words;

	public Command(string[] args){ words = args; }

	public void Exec(GameObject target, TypeConverter converter,
										float timeout, Action postAction){
		var action = words[0];
		if(action[0]=='/'){
			Debug.LogError("Pause" + action);
			action = action.Substring(1);
		}
		var ability = AbilityForName(target, action);
		int n = words.Length;
		var args = new string[n-1];
		for(int i=1; i<n; i++) args[i-1] = words[i];
		ability.Invoke(args, converter, timeout, postAction);
	}

	public static implicit operator Command(string s){
		return new Command(s.Split(' '));
	}
	
	override public string ToString(){ return string.Join(" ", words); }

	Ability AbilityForName(GameObject target, string name){
		foreach(Ability c in target.GetComponents<Ability>()){
			if(c.GetType().Name==name) return c;
		} return null;
	}

}}
