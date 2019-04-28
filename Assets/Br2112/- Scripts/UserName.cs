using UnityEngine; using UnityEngine.UI;

public class UserName : MonoBehaviour{

	const string USER_NAME = "Username";
	public GameObject panel;
	new public static string name;

	void Start(){
	    name = PlayerPrefs.GetString(USER_NAME);
		panel.SetActive(name.Length == 0);
	}

	public void SET_USERNAME(){
		var s = this.Find<InputField>("[Name input field]").text.Trim();
		if (s.Length <= 0) return;
		PlayerPrefs.SetString(USER_NAME, s);
		PlayerPrefs.Save();
		name = s;
		panel.SetActive(false);
	}

}
