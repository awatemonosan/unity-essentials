using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BroadcastService {
  private static Dispatcher dispatcher = new Dispatcher();
  
  public static void Trigger(string evnt) {
    dispatcher.Trigger(evnt);
  }
  public static void Trigger(Hashtable payload) {
    dispatcher.Trigger(payload);
  }
  public static void Trigger(string evnt, Hashtable payload) {
    dispatcher.Trigger(evnt, payload);
  }

  public void Bind(string from, string to) {
    dispatcher.Bind(from, to);
  }

  public void Unbind(string binding) {
    dispatcher.Unbind(binding);
  }

  public static int On(string evnt, Callback callback) {
    return dispatcher.On(evnt, callback);
  }

  public static void Off(int reference) {
    dispatcher.Off(reference);
  }
}
