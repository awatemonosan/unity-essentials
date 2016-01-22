using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MetaGameObject : MetaObject {
  private static List<MetaGameObject> metaGameObjects = new List<MetaGameObject>();
  private static int next_id = 0;

  private int _id;
  public int id{
    get { return _id; }
  }

  MetaGameObject() {
    this._id = next_id++;
    metaGameObjects[this._id] = this;
  }
}
