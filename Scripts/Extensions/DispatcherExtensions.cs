using UnityEngine;
using System.Collections;

public static class DispatcherExtensions {
  public static DispatcherController GetDispatcher(this GameObject that) {
    DispatcherController dispatcher = that.GetComponent<DispatcherController>();

    if (dispatcher == null)
      dispatcher = that.AddComponent<DispatcherController>();

    return dispatcher;
  }
  public static DispatcherController GetDispatcher(this Transform that) {
    return that.gameObject.GetDispatcher();
  }
  public static DispatcherController GetDispatcher(this MonoBehaviour that) {
    return that.gameObject.GetDispatcher();
  }


  public static void Trigger(this GameObject that,    string evnt) {
    that.GetDispatcher().Trigger(evnt);
  }
  public static void Trigger(this Transform that,     string evnt) {
    that.gameObject.Trigger(evnt);
  }
  public static void Trigger(this MonoBehaviour that, string evnt) {
    that.gameObject.Trigger(evnt);
  }

  public static void Trigger(this GameObject that,    Hashtable payload) {
    that.GetDispatcher().Trigger(payload);
  }
  public static void Trigger(this Transform that,     Hashtable payload) {
    that.gameObject.Trigger(payload);
  }
  public static void Trigger(this MonoBehaviour that, Hashtable payload) {
    that.gameObject.Trigger(payload);
  }

  public static void Trigger(this GameObject that,    string evnt, Hashtable payload) {
    that.GetDispatcher().Trigger(evnt, payload);
  }
  public static void Trigger(this Transform that,     string evnt, Hashtable payload) {
    that.gameObject.Trigger(evnt, payload);
  }
  public static void Trigger(this MonoBehaviour that, string evnt, Hashtable payload) {
    that.gameObject.Trigger(evnt, payload);
  }
  

  public static int On(this GameObject that,  string evnt, Callback callback) {
    return that.GetDispatcher().On(evnt, callback);
  }
  public static int On(this Transform that, string evnt, Callback callback) {
    return that.gameObject.On(evnt, callback);
  }
  public static int On(this MonoBehaviour that, string evnt, Callback callback) {
    return that.gameObject.On(evnt, callback);
  }

  public static bool Off(this GameObject that, string evnt, Callback callback) {
    return that.GetDispatcher().Off(evnt, callback);
  }
  public static bool Off(this Transform that, string evnt, Callback callback) {
    return that.gameObject.Off(evnt, callback);
  }
  public static bool Off(this MonoBehaviour that, string evnt, Callback callback) {
    return that.gameObject.Off(evnt, callback);
  }

}