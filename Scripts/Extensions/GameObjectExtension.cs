using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameObjectExtension {
  public static GameObjectExtensionController Ext(this GameObject that) {
    GameObjectExtensionController ext = that.GetComponent<GameObjectExtensionController>();

    if(ext == null)
      ext = that.AddComponent<GameObjectExtensionController>();

    return ext;
  }

  public static void SetVertexColor(this GameObject that, Color color){
    MeshFilter meshFilter = that.GetComponent<MeshFilter>();
    Debug.Log("Setting Vertex Color");
    if(meshFilter == null) return;

    Debug.Log("Mesh Exists");
    Mesh mesh = meshFilter.mesh;
     
    Color[] colors = new Color[mesh.vertices.Length];

    for(int i = 0; i < mesh.vertices.Length; i++)
    {
      Debug.Log("Setting vertex" + i);
      colors[i] = color;
    }

    mesh.colors = colors;
  }

/*===========================================================================*/
/// Physics helpers
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
/// Data helpers
/*===========================================================================*/
  public static Hashtable Data(this GameObject that) {
    return that.Ext().data;
  }

}