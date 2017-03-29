using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
  public string objectName = "";
  public string selector = "";
  public string eventName = "";
  public GameObject target;

  void Start()
  {
    target = UQuery.Query(selector).First();
    Debug.Log("SPAWNER");
    Debug.Log(target);
    target.GetDispatcher().On(eventName, this.Spawn);
  }

  private bool Spawn(UModel payload) 
  {
    Debug.Log("blam");
    GameObject.Instantiate(Resources.Load(objectName) as GameObject);
    return true;
  }
}

