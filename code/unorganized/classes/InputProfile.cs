using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class InputProfile {
  public Dispatcher dispatcher = new Dispatcher();
  private DispatcherInterface uInputInterface = new DispatcherInterface(UInput.WithInstance().GetDispatcher());
  private Dictionary<string, string> inputBindingMap = new Dictionary<string, string>();

  public void Bind(string inputName, string bindingName)
  {
    this.Unbind(inputName);
    this.uInputInterface.On(inputName, this.BoundInputCallback);
    this.inputBindingMap[inputName] = bindingName;
  }

  public void Unbind(string inputName)
  {
    this.uInputInterface.Off(inputName);
    this.inputBindingMap.Remove(inputName);
  }

  private void BoundInputCallback(UData payload)
  {
    string inputName = payload.Get<string>("eventName");
    string bindingName = inputBindingMap[inputName];
    this.dispatcher.Trigger(bindingName);
  }
}
