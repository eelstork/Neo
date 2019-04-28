using UnityEngine; using UnityEngine.UI;

public class UserName : MonoBehaviour{

	const string USER_NAME = "Username";
	public GameObject panel;
	new public static string name;

	void Start(){
	    name = PlayerPrefs.GetString(USER_NAME);
		inputField.text = name;
		InvokeRepeating("SetUserName", 5, 5);
	}

	public void SetUserName(){
		var s = inputField.text.Trim();
		if (s.Length <= 0) return;
		PlayerPrefs.SetString(USER_NAME, s);
		PlayerPrefs.Save();
		name = s;
		//panel.SetActive(false);
	}

	public static string value{get{
		return FindObjectOfType<UserName>().inputField.text;
	}}

	InputField inputField{
		get{ return this.Find<InputField>("[Name input field]"); }
	}

}
