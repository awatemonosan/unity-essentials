using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MetaObject : Dispatcher {
  private int _callbackID;

  public MetaObject() {
    _callbackID = Broadcaster.On("all", this.Trigger);
  }

  ~MetaObject() {
    Broadcaster.Off(_callbackID);
    this.Trigger("destroy");
  }
}
