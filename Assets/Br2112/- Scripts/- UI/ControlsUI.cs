using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour {

    public float D = 2;

	string[] controls = {
        "OPERATION: move/sneak forward/back | look left/right/up/down",
        "strafe left/right | nudge N/-N | jump/dig | shoot | teleport ",
        "cam left/right/up/down | zoom N.N | stabilizer on/off"
	};

    string[] puns = {
        "WEATHER BULLETIN: The year is 2112, the weather is fine",
        "WEATHER BULLETIN: The air is slightly polluted (AQI 7165)",
        "CENTRAL COMMAND: RECYCLE",
        "HYDROPONICS: Biomatter shortage detected",
        "Now playing: The end is nigh, the end is near",
    };

    void Start(){ StatusUpdate(); }

    void StatusUpdate(){
        switch(state){
            case UserState.State.ENTERING:
                M("WELCOME TO THE FACILITY; PLEASE STANDBY");
                break;
            case UserState.State.SPECTATE:
                M("CODE M: Battle in progress, please standby");
                break;
            case UserState.State.IDLE:
                M("CODE S: Population shortage, please standby");
                break;
            case UserState.State.MATCHING:
                M("IMPERATIVE: OVERPOPULATION DETECTED - REDUCE THE POPULATION");
                break;
            case UserState.State.WIN:
                M("POPULATION ONE. CONGRATULATIONS");
                break;
            case UserState.State.GHOST:
                M("CODE Z: You are toast. recycling in progress, please wait");
                break;
        }
        Invoke("ControlsUpdate0", D);
    }

    void ControlsUpdate0(){ M(controls[0]); Invoke("ControlsUpdate1", D*2); }

    void ControlsUpdate1(){ M(controls[1]); Invoke("ControlsUpdate2", D*2); }

    void ControlsUpdate2(){ M(controls[2]); Invoke("Joke", D*2); }

    void Joke(){
        M(puns[Random.Range(0, puns.Length)]);
        Invoke("StatusUpdate", D); }

    void M(string msg){ GetComponentInChildren<Text>().text = msg; }

    UserState.State state{ get{
        var p = Connection.localPlayer;
        if(!p) return UserState.State.ENTERING;
        return p.transform.Get<UserState>().state;
    }}


}
