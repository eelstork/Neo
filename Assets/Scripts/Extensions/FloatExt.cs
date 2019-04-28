
public static class FloatExtensions{

    /* Clip to the closed range [a, b] */
    public static float Clip(this float x, float a=0, float b=1){
        if(x<a) x = a;
		if(x>b) x = b;
		return x;
    }

}
