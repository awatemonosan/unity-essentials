using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MetaObject : Dispatcher {
  private DispatcherListener dispatcherListener;

  public MetaObject() {
    dispatcherListener = Broadcaster.On("all", this.Trigger);
  }

  ~MetaObject() {
    dispatcherListener.Release();
  }
}
