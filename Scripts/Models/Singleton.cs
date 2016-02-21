using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
  private static T _instance;
  private static object _lock = new object();
  private static bool isQuitting = false;

  public static T I {
    get { return Instance; }
  }

  public static T Instance {
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
    T i = Instance;
  }

  public void OnDestroy () {
    isQuitting = true;
  }
}