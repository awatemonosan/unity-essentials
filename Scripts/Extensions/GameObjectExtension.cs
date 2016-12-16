using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameObjectExtension
{
  public static Dispatcher GetDispatcher(this GameObject that)
  {
    return that.WithComponent<GameObjectExtensionController>().GetDispatcher();
  }

  public static string GetState(this GameObject that, string stateName)
  {
    return that.WithComponent<GameObjectExtensionController>().GetState(stateName);
  }

  public static void SetState(this GameObject that, string stateName, string state)
  {
    that.WithComponent<GameObjectExtensionController>().SetState(stateName, state);
  }

  public static void AddClass(this GameObject that, string className)
  {
    that.WithComponent<GameObjectExtensionController>().AddClass(className);
  }

  public static void RemoveClass(this GameObject that, string className)
  {
    that.WithComponent<GameObjectExtensionController>().RemoveClass(className);
  }

  public static bool HasClass(this GameObject that, string className)
  {
    return that.WithComponent<GameObjectExtensionController>().HasClass(className);
  }

  public static void AddID(this GameObject that, string id)
  {
    that.WithComponent<GameObjectExtensionController>().AddID(id);
  }

  public static void RemoveID(this GameObject that, string id)
  {
    that.WithComponent<GameObjectExtensionController>().RemoveID(id);
  }

  public static bool HasID(this GameObject that, string id)
  {
    return that.WithComponent<GameObjectExtensionController>().HasID(id);
  }

//           |
// Old stuff |
//           V

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
    return that.WithComponent<GameObjectExtensionController>().data;
  }

  public static Selection Query(this GameObject that, string queryString) { return that.Query(queryString); }
  public static Selection Query(this GameObject that, string[] queryArray) { return that.Query(queryArray); }

  public static T WithComponent<T>(this GameObject that) { return that.WithComponent<T>(); }

}