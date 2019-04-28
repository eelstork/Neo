using System; using System.Linq;
using UnityEngine; using UnityEditor;

public static class ComponentExt{

    public static float Angle(this Component self, Transform that,
                                                   bool planar=true){
        return self.Angle(that.position, planar);
    }

    public static float Angle(this Component self, Vector3 that,
                                                   bool planar = true){
        Vector3 u = self.transform.forward, v = self.Dir(that);
        if(planar){ u.y = v.y = 0; u.Normalize(); v.Normalize(); }
        return Vector3.Angle(u, v);
    }

    public static void Call(this Component self, string func, object[] P,
                                                              TypeConverter c){
        var t = self.GetType();
        var M = t.GetMethods();
        int namesakes = 0;
        foreach(var m in M){
            var T = m.GetParameters();
            if(m.Name == func){
                namesakes++;
                if(T.Length==P.Length){
                    var P2 = c.Convert(P, T);
                    m.Invoke(self, P2);
                    return;
                }
            }
        }
        if(namesakes>0) throw new Exception(string.Format(
            "{0} func(s) named {1} in {2} but param count mismatch: {3} ",
            namesakes, func, t.Name, P.Length));
        throw new Exception("No such func:" + func);
    }

    public static bool CanSee(this Transform self, Transform t, Vector3 up){
        RaycastHit result;
        var eye = self.position + up;
        Debug.DrawLine(eye, self.Dir(t));
        bool didHit = Physics.Raycast(eye, (t.position-eye), out result);
        if(!didHit) return true;
        return result.collider == t.GetComponentInChildren<Collider>();
    }

    public static Transform Closest<C>(
           this Component self, Func<C, bool> cond = null) where C : Component{
        C[] arr = GameObject.FindObjectsOfType<C>();
        C sel = null; float min = 0;
        foreach(C e in arr){
            if(e.transform==self.transform) continue;
            if(cond!=null && !cond(e)) continue;
            float d = self.transform.Dist(e);
            if(!sel || d<min){
                sel = e; min = d;
            }
        } return !sel ? null : sel.transform;
    }

    public static float Density(this Component self){
        return self.Get<Rigidbody>().mass/self.Volume();
    }

    public static Vector3 Dir(this Component self, Transform that){
        return (that.position - self.transform.position).normalized;
    }

    public static Vector3 Dir(this Component self, Vector3 that){
        return (that - self.transform.position).normalized;
    }

    public static float Dist(this Component self, Component other,
                                                          bool planar = false){
        Vector3 P = self.transform.position, Q = other.transform.position;
        if(planar) P.y = Q.y;
        return Vector3.Distance(P, Q);
    }

    public static float Dist(this Component self, Vector3 other,
                                                          bool planar = false){
        Vector3 P = self.transform.position;
        if(planar) P.y = other.y;
        return Vector3.Distance(P, other);
    }

    public static C Find<C>(this Component x) where C : Component{
        return GameObject.FindObjectOfType<C>();
    }

    public static GameObject Find(this Component x, string s){
        return GameObject.Find(s);
    }

    public static C Find<C>(this Component x, string s) where C : Component{
        return GameObject.Find(s).GetComponent<C>();
    }

    public static void GainMass(this Component self, Component other,
                                                                  float ratio){
        Rigidbody thisBody = self.GetComponentInChildren<Rigidbody>(),
                  thatBody = other.GetComponentInChildren<Rigidbody>();
        float m = thatBody.mass*ratio;
        self.GainVolumeFromMass(m);
        other.GainVolumeFromMass(-m);
        thisBody.mass += m;
        thatBody.mass -= m;
    }

    /*
     * Assuming a gain of 'mass' and constant density, match a size increase
     * (or decrease) with weight gain (or loss).
     * Returns the uniform scalar used to resize the object.
     */
    public static float GainVolumeFromMass(this Component self, float mass){
        var v = self.Volume();             // initial volume
        var w = v + mass/self.Density();   // final volume
        var s = Mathf.Pow(w/v, 1/3f);      // uniform size increase
        var u = self.transform.localScale;
        self.transform.localScale = u * s;
        return s;
    }

    public static C Get<C>(this Component self) where C : Component{
        var c = self.GetComponent<C>();
        return c ? c : self.GetComponentInChildren<C>();
    }

    /* Find the component of type C on object named obj*/
    public static C Get<C>(this Component x, string obj)
        where C : Component{
        return GameObject.Find(obj).GetComponent<C>();
    }

    public static string Name(this Component self){
        return self.gameObject.name;
    }

    public static C Req<C>(this Component x) where C : Component{
        var c = x.GetComponent<C>();
        return c ? c : x.gameObject.AddComponent<C>();
    }

    public static float Volume(this Component self){
        var s = self.Get<Collider>().bounds.size; return s.x * s.y * s.z;
    }

    public static void Taint(this Component self, Color col, float amount){
        if(amount<=0) return;
		var R = self.GetComponentsInChildren<Renderer>();
		foreach(var r in R){
			var m = r.material;
			m.color = m.color*(1-amount) + col * amount;
			r.material = m;
		}
    }

}
