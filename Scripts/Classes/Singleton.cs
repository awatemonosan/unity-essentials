using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
  static private T _instance;
  static private object _lock = new object();
  static private bool isQuitting = false;

  static public T instance {
    get {
      if (isQuitting) return null;
 
      lock(_lock) {
        if (_instance == null) {
          T[] instances = (T[])FindObjectsOfType(typeof(T));

          if (instances.Length == 0) {
            GameObject singleton = new GameObject();
            singleton.name = typeof(T).ToString();
            DontDestroyOnLoad(singleton);

            _instance = singleton.AddComponent<T>();
          } else {
            _instance = instances[0];
          }
        }
 
        return _instance;
      }
    }
  }

  public static void EnsureExists(){
    T i = instance; // To create it you gotta reference it. EZPZ
  }

  static public Dispatcher GetDispatcher() {
    return instance.GetDispatcher();
  }

  public void OnDestroy () {
    isQuitting = true;
  }
}