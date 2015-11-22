using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Colorize_EXT {
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

  public static void SetVertexColor(this MonoBehaviour that, Color color) {
    that.gameObject.SetVertexColor(color);
  }

}