using System.Collections;
using System.Collections.Generic;

public static class AnimationData {

	public static Dictionary<string, string[]> map =
		new Dictionary<string, string[]>(){
			{"expel",   new string[]{"attack"}},
			{"gesture", new string[]{"attack"}},
			{"give",    new string[]{"attack"}},
			{"grab",    new string[]{"attack"}},
			{"idle",    new string[]{"idle_1", "idle_2", "swim", "swimming",
			                         "sing"}},
			{"ingest",  new string[]{"grazing", "attack"}},
			{"lookAt",  new string[]{"attack"}},
			{"push",    new string[]{"attack"}},
			{"tell",    new string[]{"attack"}},
			{"walk",    new string[]{"walking", "run", "running", "swim",
									 "swimming", "fly"}},
		};

}
