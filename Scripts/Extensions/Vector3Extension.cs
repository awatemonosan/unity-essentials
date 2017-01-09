using UnityEngine;
using System.Collections;

public static class Vector3Extension 
{
  
  public static Vector3 Random(this Vector3 that)
  {
    Vector3 v = new Vector3();

	v.x = that.x * UnityEngine.Random.Range(-1.0f, 1.0f);
	v.y = that.y * UnityEngine.Random.Range(-1.0f, 1.0f);
	v.z = that.z * UnityEngine.Random.Range(-1.0f, 1.0f);

    return v.normalized;
  }

  public static Vector3 Clone(this Vector3 that)
  {
    return new Vector3(that.x,that.y,that.z);
  }

  public static Vector3 Floor(this Vector3 that) 
  {
    that.x=Mathf.Floor(that.x);
    that.y=Mathf.Floor(that.y);
    that.z=Mathf.Floor(that.z);

    return that;
  }

}