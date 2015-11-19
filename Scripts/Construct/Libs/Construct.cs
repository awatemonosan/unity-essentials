using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Construct {
  private static Dictionary<String, GameObject> prefabs = new Dictionary<String,GameObject>();
  private static Dictionary<int, GameObject> createdObjects = Dictionary<int, GameObject>();
  // private static Dictionary<String, List<GameObject>> objectsByType = Dictionary<String, List<GameObject>>();

	public static void Create (string prefabName) {
    GameObject newObject;

    if(prefabs.ContainsKey(prefabName)) {
	    newObject = GameObject.Instantiate(prefabs[prefabName]);
      newObject.AddComponent<ConstructObject>();
      createdObjects[newObject.ID] == newObject;
    }

    return newObject;
	}

  public static void Register(GameObject prefab) {
    prefabs.Add(prefab.name,prefab);
  }

  public static GameObject Get(int ID) {
    GameObject object;

    if(createdObjects.ContainsKey(ID))
      object = createdObjects[ID]

    return object;
  }

}
