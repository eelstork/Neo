using System;
using System.Reflection;
using UnityEngine;

public class TypeConverter : MonoBehaviour {

	public object[] Convert(object[] X, ParameterInfo[] T){
		object[] Y = new object[X.Length];
		for(int i=0; i<T.Length; i++)
			Y[i] = Convert(X[i], T[i]);
		return Y;
	}

	public object Convert(object x, ParameterInfo pi){
		var t = pi.ParameterType;
		var s = x as String;
		if(s==null) return null;
		if(t==typeof(String)) return s.Replace('_', ' ');
		var go = GameObjectForName(s);
		// TODO: do not muddle through
		if(!go) return null; //throw new Exception("No such game object: "+s);
		if(t==typeof(Transform)) return go.transform;
		if(t==typeof(Vector3))   return go.transform.position;
		if(t==typeof(Target))   return (Target)(go.transform);
		throw new Exception("No conversion for "+t.Name);
	}

	GameObject GameObjectForName(string name){
		var sensor = this.Get<Sensor>();
		GameObject go = null;
		if(sensor){
			var t = sensor.Any(name);
			if(t) go = t.gameObject;
		}else go = GameObject.Find(name);
		if(go) return go;
		foreach(Transform child in transform){
			if(child.gameObject.name==name) return child.gameObject;
		} return null;
	}

}
