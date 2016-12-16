using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class DispatcherInterface {
  private Dispatcher dispatcher;
  private Dictionary<string, DispatcherListener> disptcherListeners = new Dictionary<string, DispatcherListener>();

  public DispatcherInterface(Dispatcher dispatcher)
  {
    this.dispatcher = dispatcher;
  }

  ~DispatcherInterface()
  {
    foreach(KeyValuePair<string, DispatcherListener> entry in disptcherListeners)
    {
      DispatcherListener disptcherListener = entry.Value;
      disptcherListener.Release();
    }  
  }

  public void On(string eventName, Callback callback)
  {
    this.Off(eventName);
    this.disptcherListeners[eventName] = dispatcher.On(eventName, callback);
  }

  public void Off(string eventName)
  {
    this.disptcherListeners[eventName].Release();
  }
}
