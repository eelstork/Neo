using UnityEngine;
using Photon.Pun;

public class ActionContext : MonoBehaviour {

    public Transform content{ get{
        foreach(Transform child in transform){
            if(child.localPosition == Vector3.zero
                                  && PhotonView.Get(child)!=null) return child;
        }
        throw new Invalid("No content");
    }}

    public Transform hand { get{
        foreach(Transform child in transform){
            var r = child.Get<Renderer>();
            if(r && r.enabled) return child;
        }
        throw new Invalid("Nothing equipped");
    }}

    public Transform other { get{ return this.Closest<Avatar>(); }}

    public Transform that { get{
        var x = this.Closest<Rigidbody>( e => (e.mass < body.mass*0.5f) );
        if(!x) throw new Invalid("Nothing nearby");
        return x;
    }}

    Transform self{ get{ return transform; }}
    Rigidbody body{ get{ return this.Get<Rigidbody>(); }}

    public class Invalid : System.Exception{
        public Invalid(string msg) : base(msg) {}
    }

}
