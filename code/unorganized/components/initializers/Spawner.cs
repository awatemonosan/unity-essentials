using UnityEngine;
using System.Collections;

using Ukulele;

public class Spawner : MonoBehaviour {
  public string objectName = "";
  public string eventName = "";

  void Start()
  {
    this.On(eventName, this.Spawn);
  }

  private void Spawn(Hashtable payload) 
  {
    Debug.Log("blam");
    GameObject.Instantiate(Resources.Load(objectName) as GameObject);
  }
}

