using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TransformExtension {
  public static TransformExtensionController Ext(this Transform that) {
    TransformExtensionController ext = that.GetComponent<TransformExtensionController>();

    if(ext != null)
      return ext;

    return that.gameObject.AddComponent<TransformExtensionController>();
  }
  public static Transform Up(this Transform that) {
    if(that.parent != null)
      return that.parent;
    return null;
  }
  public static Transform Up(this Transform that, int count) {
    if(count>0)
      return that.Up().Up(count-1);
    return that;
  }
  public static bool IsHead(this Transform that) {
    return that.IsTop() || that.Ext().isHead;
  }
  public static Transform Head(this Transform that) {
    if(that.IsHead()) return that;
    return that.Up().Head();
  }
  public static bool IsTop(this Transform that) {
    return that.Up() == null;
  }
  public static Transform Top(this Transform that) {
    if(that.IsTop()) return that;
    return that.Up().Top();
  }

}