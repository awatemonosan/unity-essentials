using UnityEngine;
using System.Collections;

public static class Color_EXT {
  
  public static Color Clone(this Color that){
    return new Color(that.r, that.g, that.b, that.a);
  }
  
  public static Color Apply(this Color that, JSONObject json){

    that.r = json.GetField("colorR").f;
    that.g = json.GetField("colorG").f;
    that.b = json.GetField("colorB").f;

    return that;
  }

  public static Color Random(this Color that){
    Vector3 v = new Vector3().Random();

    that.r=v.x;
    that.g=v.y;
    that.b=v.z;
    that.a=1.0f;

    return that;
  }

}