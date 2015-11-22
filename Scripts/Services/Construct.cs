using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Construct {
  private static Dictionary<string, GameObject> prefabs = new Dictionary<string,GameObject>();
  private static Dictionary<int, GameObject> createdObjects = new Dictionary<int, GameObject>();
  // private static Dictionary<String, List<GameObject>> objectsByType = Dictionary<String, List<GameObject>>();
  
  public static void Start(){

  }

	public static GameObject Create (string prefabName) {
    GameObject newObject = null;

    if(prefabs.ContainsKey(prefabName)) {
	    newObject = GameObject.Instantiate(prefabs[prefabName]);
      newObject.name = prefabName;
      ConstructObjectController newCostObjControler = newObject.AddComponent<ConstructObjectController>();
      createdObjects[newCostObjControler.ID] = newObject;
    }

    return newObject;
	}

  public static void Register(string name, GameObject prefab) {
    prefabs.Add(name,prefab);
  }

  public static void Register(GameObject prefab) {
    Register(prefab.name, prefab);
  }

  public static GameObject Get(int ID) {
    GameObject obj = null;

    if(createdObjects.ContainsKey(ID))
      obj = createdObjects[ID];

    return obj;
  }
}
