using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
  public string objectName = "";
  public string eventName = "";

  void Start()
  {
    this.gameObject.GetDispatcher().On(eventName, this.Spawn);
  }

  private void Spawn(UData payload) 
  {
    Debug.Log("blam");
    GameObject.Instantiate(Resources.Load(objectName) as GameObject);
  }
}

