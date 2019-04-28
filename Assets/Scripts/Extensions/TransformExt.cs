using UnityEditor;
using UnityEngine;

public static class TransformExt{

    /*
     * Move towards the target, unless within threshold; true if the transform
     * did move.
     */
    public static bool MoveTowards(this Transform self, Transform that,
                                  float speed=1, float threshold=1){
        if(self.Dist(that) <= threshold) return false;
        self.position += self.Dir(that) * speed * Time.deltaTime;
        return true;
    }

    public static bool MoveTowards(this Transform self, Vector3 that,
                                  float speed=1, float threshold=1){
        if(self.Dist(that) <= threshold) return false;
        self.position += self.Dir(that) * speed * Δt;
        return true;
    }

    public static bool RotateTowards(this Transform self, Vector3 that,
                                     float speed = 1){
        Vector3 u = Vector3.RotateTowards(self.forward, self.Dir(that), Δt, 0);
        self.LookAt(self.position+u, self.up);
        var α = self.Angle(that);
        return α>1;
    }

    /*
     * Steer towards the target, unless within threshold; true if a force was
     * applied
     */
    public static bool SteerTowards(
        this Transform self, Transform that,
        float speed=1, float threshold=1, float traction=50, float strength=25)
    {
        if(self.Dist(that) <= threshold) return false;
		var body = self.GetComponent<Rigidbody>();
		var Δ = self.Dir(that)*speed-body.velocity;
        if(Δ.magnitude>strength) Δ = Δ.normalized*strength;
		body.AddForce(Δ*traction*body.mass);
        return true;
    }

    static float Δt{ get{ return Time.deltaTime; }}

}
