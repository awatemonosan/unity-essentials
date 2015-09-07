using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

  public static int ToIndex(this Color that, List<Color> pallet){
    float bestDiff = Mathf.Infinity;
    int bestIndex = 0;

    for(int index = 0; index < pallet.Count; index++){
      float r = Mathf.Abs(that.r-pallet[index].r);
      float g = Mathf.Abs(that.g-pallet[index].g);
      float b = Mathf.Abs(that.b-pallet[index].b);
      float a = Mathf.Abs(that.a-pallet[index].a);
      float diff = r+g+b+a;

      if(diff<bestDiff){
        bestDiff = diff;
        bestIndex = index;
      }
    }

    return bestIndex;
  }
}