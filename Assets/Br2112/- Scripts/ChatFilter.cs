using UnityEngine;

public class ChatFilter : MonoBehaviour {

	const int COOLDOWN = 2;
	const int PROFANITY_BONUS = 50;
	public float stamp;
	public AudioSource W;
	public AudioSource F;
	public AudioSource O;

	public void Process(string str){
		if(str.Length>40){
			hp.Pay(str.Length-40);
			O.Play();
			return;
		}
		float t = Time.time; if(t<stamp) return;
		if(ContainsProfanity(str)){
			if(Random.value>0.4){
				if(hp.value<15){
					print("Profanity and less than 15hp: KO");
					hp.value = 0;
				}else{
					print("Profanity costs you 2/3 hp"); hp.Div(3);
				} F.Play();
			}else{ print("Profanity bonus, 50 hp");
				hp.Grant(PROFANITY_BONUS);
				W.Play();
			}
		}else{
			hp.Grant(str.Length/5);
		}
		stamp = t + COOLDOWN;
	}

	HP hp{ get{ return this.Get<HP>(); }}

	bool ContainsProfanity(string x){
		x = x.ToLower();
		foreach(var k in Profanity.all){
			if(x.Contains(k)) return true;
		} return false;
	}

}
