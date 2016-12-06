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

  public static int count = 0;

  public static void Trigger(this GameObject that, string evnt) {
    that.GetDispatcher().Trigger(evnt);
  }
  public static void Trigger(this GameObject that, Hashtable payload) {
    that.GetDispatcher().Trigger(payload);
  }
  public static void Trigger(this GameObject that, string evnt, Hashtable payload) {
    that.GetDispatcher().Trigger(evnt, payload);
  }
  public static int On(this GameObject that,  string evnt, Callback callback) {
    return that.GetDispatcher().On(evnt, callback);
  }
  public static int On(this GameObject that,  string evnt, Callback callback, Hashtable payload) {
    return that.GetDispatcher().On(evnt, callback, payload);
  }
  public static void Off(this GameObject that, int callbackID) {
    that.GetDispatcher().Off(callbackID);
  }
  // public static void Bind(this GameObject that, string from, string to) {
  //   that.gameObject.Bind(from, to);
  // }
  // public static void Unbind(this GameObject that, string binding) {
  //   that.gameObject.Unbind(binding);
  // }

  public static void Trigger(this Transform that, string evnt) {
    that.gameObject.Trigger(evnt);
  }
  public static void Trigger(this Transform that,  Hashtable payload) {
    that.gameObject.Trigger(payload);
  }
  public static void Trigger(this Transform that,  string evnt, Hashtable payload) {
    that.gameObject.Trigger(evnt, payload);
  }
  public static int On(this Transform that, string evnt, Callback callback) {
    return that.gameObject.On(evnt, callback);
  }
  public static int On(this Transform that, string evnt, Callback callback, Hashtable payload) {
    return that.gameObject.On(evnt, callback, payload);
  }
  public static void Off(this Transform that, int callbackID) {
    that.gameObject.Off(callbackID);
  }
  // public static void Bind(this Transform that, string from, string to) {
  //   that.gameObject.Bind(from, to);
  // }
  // public static void Unbind(this Transform that, string binding) {
  //   that.gameObject.Unbind(binding);
  // }

  public static void Trigger(this Component that, string evnt) {
    that.gameObject.Trigger(evnt);
  }
  public static void Trigger(this Component that,  Hashtable payload) {
    that.gameObject.Trigger(payload);
  }
  public static void Trigger(this Component that,  string evnt, Hashtable payload) {
    that.gameObject.Trigger(evnt, payload);
  }
  public static int On(this Component that, string evnt, Callback callback) {
    return that.gameObject.On(evnt, callback);
  }
  public static int On(this Component that, string evnt, Callback callback, Hashtable payload) {
    return that.gameObject.On(evnt, callback, payload);
  }
  public static void Off(this Component that, int callbackID) {
    that.gameObject.Off(callbackID);
  }
}