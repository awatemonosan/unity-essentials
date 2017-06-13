using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ukulele;

public class UWorld : Singleton<UWorld> {
  static public UTerrainController WithTerrain () {
    return UWorld.WithInstance().gameObject.WithComponent<UTerrainController>();
  }

  static public void SetDayLength(float seconds) {
    WithInstance().secondsPerDay = seconds;
  }

  static public void SetTime(float hour) {
    WithInstance().timeOfDay = hour % 24;
  }

  static public void AdvanceTime(float delta) {
    if(WithInstance().secondsPerDay == 0 ) return;
    SetTime( WithInstance().timeOfDay + ((delta * 24) / WithInstance().secondsPerDay) );
  }

  public float timeOfDay = 12f;
  public float secondsPerDay = 0f; //60f*24f;

  public Light sunLight;
  public Material starField;

  void Start() {
    this.sunLight = GameObject.Find("SunLight").WithComponent<Light>();
    this.starField = GameObject.Find("StarField").GetComponent<Renderer>().sharedMaterial;
    RenderSettings.fog = true;
  }

  void Update() {
    UWorld.AdvanceTime(Time.deltaTime);

    if(this.sunLight) {
      float angle = this.timeOfDay/24*360-90;
      this.sunLight.transform.position = Camera.main.transform.position;
      this.sunLight.transform.rotation = Quaternion.Lerp(this.sunLight.transform.rotation, Quaternion.Euler(angle, 15, 0), Time.deltaTime);

      float sunIntensity = Vector3.Dot(Vector3.up * -1, this.sunLight.transform.rotation*Vector3.forward);
      sunIntensity += 0.25f;
      sunIntensity = UMath.Clamp(sunIntensity, 0, 1);

      float fogDensity = Vector3.Dot(Vector3.up, this.sunLight.transform.rotation*Vector3.forward) + 1;
      fogDensity /= 2;

      // Mathf.Sin(2f * Mathf.PI / angle);
      // float sunIntensity = UMath.Clamp(this.timeOfDay, 4, 20);
      //   sunIntensity -= 16;
      //   sunIntensity = 8 - Mathf.Abs(sunIntensity);
      //   sunIntensity = Mathf.Min(sunIntensity, 4)
      //   sunIntensity = sunIntensity / 4;
        // sunIntensity = 0.25f + sunIntensity*0.75f;

      this.sunLight.intensity = sunIntensity * 1f;
      this.sunLight.bounceIntensity = sunIntensity * 1.5f;
      RenderSettings.ambientIntensity = sunIntensity * 0.33f;
      RenderSettings.fogColor = sunLight.color * sunIntensity;
      RenderSettings.fogDensity = sunIntensity * 0.0025f;
      // RenderSettings.fogColor = (sunLight.color * sunIntensity + new Color(49/2048, 77/2048f, 121/2048f) * fogDensity);
      // RenderSettings.fogDensity = fogDensity * 0.01f;

      this.starField.SetColor("_Tint", new Color(1, 1, 1, 1-sunIntensity));
    }
  }
}
