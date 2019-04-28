using UnityEngine;

public abstract class Target{

	public abstract bool IsValid();

	protected abstract Vector3 Value();

	// Properties -------------------------------------------------------------

	public virtual Collider  collider  { get{ return null; } }
	public virtual Transform transform { get{ return null; } }

	// Conversions ------------------------------------------------------------

	public static implicit operator bool(Target t){ return t!=null; }

	public static implicit operator Vector3(Target t){ return t.Value(); }

	public static implicit operator Target(Vector3 t){
		return new TargetPoint(t);
	}

	public static implicit operator Target(Transform t){
		if(t==null) return null;
		return new TargetTransform(t);
	}

}

// ----------------------------------------------------------------------------

public class TargetPoint : Target{

	Vector3 data;

	public TargetPoint(Vector3 p){ data = p; }

	override public bool IsValid(){ return true; }

	override public string ToString(){ return data.ToString(); }

	override protected Vector3 Value(){ return data; }

}

// ----------------------------------------------------------------------------

public class TargetTransform: Target{

	Transform data;

	public TargetTransform(Transform t){ data = t; }

	override public bool IsValid(){ return data!=null; }

	override public Collider collider{
		get{ return transform.GetComponentInChildren<Collider>(); }
	}

	override public Transform transform { get{ return data; } }

	override public string ToString(){ return data.gameObject.name; }

	override protected Vector3 Value(){ return data.position; }

}
