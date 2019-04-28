﻿using UnityEngine;
using UnityEngine.UI;

public class ChatInput : MonoBehaviour{

	public GameObject chatInput;
	public InputField field;
	public bool alwaysVisible = true;

	void Start(){
		field.onEndEdit.AddListener(delegate{ OnSubmit(); });
		editing = false;
	}

	void Update(){
		if (editing){
			if (Input.GetKeyDown(KeyCode.Escape)) editing = false;
		}else{
			if(Input.GetKeyDown(KeyCode.Return)) editing = true;
		}
	}

	void OnSubmit(){
		var str = field.text.Trim();
		if (str.Length > 0 && Connection.localPlayer) {
			if(str.StartsWith("@")){
	            var skin = str.Substring(1);
	            GameObject.FindObjectOfType<SkinLoader>().ApplySkin(skin);
	        }else{
				bool accepted = false;
				try{
					accepted =
						Connection.localPlayer.transform.Get<TBC>().Submit(
						str.Split(' '));
				}catch(System.Exception ex){
					Debug.LogWarning("While processing command: " + ex);
				}
				if(!accepted){
					Connection.localPlayer.transform.Req<A1.Tell>().Invoke(
					UserName.name, str);
				}
			}
		}

		field.text = "";
		field.ActivateInputField();
	}

	void DetectInputStart(){
		if(Input.GetKeyDown(KeyCode.Return)) editing = true;
	}

	bool editing{
		get{ return chatInput.activeInHierarchy; }
		set{
			chatInput.SetActive(value || alwaysVisible);
			if(value)field.ActivateInputField();
			else field.DeactivateInputField();
			var c = FindObjectOfType<A1.ActorController2>();
			if(c) c.enabled = !editing;
		}
	}

}
