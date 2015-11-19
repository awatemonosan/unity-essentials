using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class QuadSprite : MonoBehaviour {
  public Shader forceShaker;
  void Update () {
    Renderer renderer = GetComponent<Renderer>();
    Material material = renderer.sharedMaterial;
    material.shader = forceShaker;
    renderer.shadowCastingMode =  UnityEngine.Rendering.ShadowCastingMode.On;
    renderer.receiveShadows = true;
    renderer.castShadows = true;
  }
}
