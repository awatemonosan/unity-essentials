using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MetaObject : Dispatcher {
  private int _callbackID;

  public MetaObject() {
    _callbackID = Broadcaster.I.On("all", this.Trigger);
  }

  ~MetaObject() {
    Broadcaster.I.Off(_callbackID);
    this.Trigger("destroy");
  }
}
