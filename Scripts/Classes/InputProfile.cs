using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class InputProfile
 {
  public Dispatcher dispatcher = new Dispatcher();
  private DispatcherInterface broadcasterInterface = new DispatcherInterface(Broadcaster.GetDispatcher());
  private Dictionary<string, string> inputBindingMap = new Dictionary<string, string>();

  public void Bind(string inputName, string bindingName)
  {
    this.Unbind(inputName);
    this.broadcasterInterface.On(inputName, this.BoundInputCallback);
    this.inputBindingMap[inputName] = bindingName;
  }

  public void Unbind(string inputName)
  {
    this.broadcasterInterface.Off(inputName);
    this.inputBindingMap.Remove(inputName);
  }

  private bool BoundInputCallback(Hashtable payload)
  {
    string inputName = payload.GetString("eventName");
    string bindingName = inputBindingMap[inputName];
    this.dispatcher.Trigger(bindingName);
    return true;
  }
}
