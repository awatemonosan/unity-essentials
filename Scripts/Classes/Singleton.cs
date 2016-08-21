using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
  static private T _instance;
  static private object _lock = new object();
  static private bool isQuitting = false;

  static public T I {
    get { return Instance; }
  }

  static public T Instance {
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
    T i = I; // To create it you gotta reference it. EZPZ
  }

  static public int On(string evnt, Callback callback, Hashtable payload) {
    return I.gameObject.On(evnt, callback, payload);
  }

  static public int On(string evnt, Callback callback) {
    return I.gameObject.On(evnt, callback);
  }

  static public void Off(int reference) {
    I.gameObject.gameObject.Off(reference);
  }

  static public void Trigger(string evnt){
    I.gameObject.Trigger(evnt);
  }

  static public void Trigger(Hashtable payload){
    I.gameObject.Trigger(payload);
  }

  static public void Trigger(string evnt, Hashtable payload){
    I.gameObject.Trigger(evnt, payload);
  }

  // static public void Bind(string from, string to) {
  //   I.gameObject.Bind(from, to);
  // }

  // static public void Unbind(string binding) {
  //   I.gameObject.Unbind(binding);
  // }

  public void OnDestroy () {
    isQuitting = true;
  }
}