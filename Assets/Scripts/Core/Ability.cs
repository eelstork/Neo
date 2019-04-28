using UnityEngine;
using Photon.Pun;
using Action = System.Action;
using Net = Photon.Pun.PhotonNetwork;
using SMO = UnityEngine.SendMessageOptions;

// TODO: uses of Invoke/CancelInvoke (the Unity timer) in this class don't feel
// safe.
namespace A1 { public class Ability : MonoBehaviour {

	const string ACTION  = "action";
	const float defaultDuration = 0.5f;
	//
	RPCParams rpc;
	Action postAction;

	public float reachFactor = 0.5f;
	//float maxAngle = 10;
	[Header("Debug")]
	public bool pauseWhenDone = false;

	public static void StopAll(Component c){
		foreach(var ability in c.GetComponents<Ability>()){
			ability.enabled = false;
		}
		var motor = c.GetComponent<MotorSystem>();
		if(motor) motor.Stop();
		var status = c.GetComponent<AgentStatus>();
		if(status) status.Clear();
	}

	public void Invoke(object[] args, TypeConverter cx, float timeout,
															Action postAction){
		this.Call("Invoke", args, cx);
		this.postAction = postAction;
		Invoke("Timeout", timeout);
	}

	// -----------------------------------------------------------------------

	protected float Dist(Target target){
		Vector3 A = (this.collider && this.collider.enabled) ?
			this.collider.ClosestPoint(target) : transform.position;
	 	Vector3 B = (target.collider && target.collider.enabled) ?
			target.collider.ClosestPoint(A) : (Vector3)target;
		Debug.DrawLine(A, B, Color.white);
		return Vector3.Distance(A, B);
	}

	protected void ExecPostAction(){
		CancelInvoke(); if(postAction!=null) postAction();
	}

	protected void Notify(string funcName, object arg){
		gameObject.SendMessage(funcName, arg, SMO.DontRequireReceiver);
	}

	protected C Req<C>() where C : Component{
        var c = GetComponent<C>(); return c ? c : gameObject.AddComponent<C>();
    }

	// Note: in general, an action such as attacking, grabbing, etc... has a
	// duration which can be affected by a number of factors. Here the
	// implementation is taking a shortcut by directly calling into an ad-hoc
	// animation handler, and matching the action length to the animation
	// length. For dependencies to be well formed:
	// - message an event to indicate that the activity is starting.
	// - retrieve the action duration from a dedicated delegate, which usually
	// is (but needn't be) the animation handler.
	protected void RPC(string m, params object[] p){
		if(rpc!=null) return;
		var activity = m.Substring(2).ToLower();
		var handler = this.Get<BasicAnimationHandler>();
		float duration = 0;
		if(handler)
			duration = this.Get<BasicAnimationHandler>().Play(activity);
		if(duration<=0) duration = defaultDuration;
		rpc = new RPCParams(m, p);
		Invoke("SendRPC", duration);
	}

	protected void ReachRPC(Transform target, string m, params object[] p){
		if(!target || rpc!=null) return;
		if(ReadyToActOn(target)){
			motor.Stop(); RPC(m, p); status.Clear();
		}else{
			motor.Target(target);
			status.Pending(this, target);
		}
	}

	protected void ReachAction(Target target, System.Action act){
		if(target==null) return;
		if(ReadyToActOn(target)){
			Debug.Log("Ready to perform reach action: "+Dist(target));
			motor.Stop(); act(); status.Clear();
			ExecPostAction();
		}else{
			motor.Target(target);
			status.Pending(this, target);
		}
	}

	protected X Req<X>(X x){
		if(x==null) throw new System.Exception("Null argument"); return x;
	}

	// PRIVATE ----------------------------------------------------------------

	void Timeout(){
		if(postAction!=null) postAction(); this.enabled = false;
	}

	void OnDisable(){ rpc = null; CancelInvoke(); postAction = null; }

	bool ReadyToActOn(Target target){
		return Dist(target)<=reach; // && this.Angle(target)<=maxAngle;
	}

	void SendRPC(){
		proxy.RPC(rpc.name, RpcTarget.All, rpc.@params); rpc = null;
		if(pauseWhenDone)Debug.LogError("Done: " + GetType().Name);
		ExecPostAction();
	}

	// ------------------------------------------------------------------------

	new protected Collider collider { get{ return this.Get<Collider>(); }}

	protected Rigidbody  body   { get{ return Req<Rigidbody>();     }}

	protected PhotonView proxy  { get{ return PhotonView.Get(this); }}

	protected MotorSystem motor { get{ return Req<MotorSystem>();   }}

	AgentStatus status{ get{ return Req<AgentStatus>(); }}

	float reach{ get{ return collider.bounds.size.x*reachFactor; }}

	// ------------------------------------------------------------------------

	class RPCParams{
		public string   name;
		public object[] @params;
		public RPCParams(string n, object[] p){
			this.name = n; this.@params=p;
		}
	}

}}
