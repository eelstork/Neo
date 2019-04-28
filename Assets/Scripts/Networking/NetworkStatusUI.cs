using UnityEngine;
using UnityEngine.UI;

public class NetworkStatusUI : MonoBehaviour {

	Text text;
	Connection connection;

	void Start(){
		text = this.Get<Text>();
		connection = this.Find<Connection>();
	}

	void Update () {
		if(connection.status == Connection.Status.Rooming)
			text.text = connection.ToString();
		else text.text = ""+connection.status;
	}
}
