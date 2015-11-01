using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameObject_EXT {
/*===========================================================================*/
// Physics helpers
/*===========================================================================*/
  public static void IgnoreCollision(this GameObject that, GameObject other, bool ignore=true) {
    Collider thatCollider = that.GetComponent<Collider>();
    Collider otherCollider = other.GetComponent<Collider>();

    if(thatCollider != null && otherCollider != null) {
      Physics.IgnoreCollision(thatCollider, otherCollider, ignore);
    }

    foreach(Transform thatChild in that.transform) {
      foreach(Transform otherChild in other.transform) {
        that.IgnoreCollision(otherChild.gameObject, ignore);
        other.IgnoreCollision(thatChild.gameObject, ignore);
        thatChild.gameObject.IgnoreCollision(otherChild.gameObject, ignore);
      }
    }
  }
/*===========================================================================*/
// GameObject tree traversal
/*===========================================================================*/
  public static GameObject Up(this GameObject that) {
    if(that.transform.parent != null)
      return that.transform.parent.gameObject;
    return null;
  }
  public static GameObject Up(this GameObject that, int count) {
    if(count>0)
      return that.Up().Up(count-1);
    return that;
  }
  public static bool IsHead(this GameObject that) {
    return that.IsTop() || that.Data().GetBool("IS_HEAD");
  }
  public static GameObject Head(this GameObject that) {
    if(that.IsHead()) return that;
    return that.Up().Head();
  }
  public static bool IsTop(this GameObject that) {
    return that.Up() == null;
  }
  public static GameObject Top(this GameObject that) {
    if(that.IsTop()) return that;
    return that.Up().Top();
  }

/*===========================================================================*/
// Data helpers
/*===========================================================================*/

  public static Hashtable Data(this GameObject that) {
    // TODO: Caching
    GameObjectData data = that.GetComponent<GameObjectData>();
    if(data == null){
      data = that.AddComponent<GameObjectData>();
    }
    return data.data;
  }

}