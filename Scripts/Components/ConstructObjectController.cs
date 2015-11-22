using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConstructObjectController : MonoBehaviourExtended {
  static int nextID = 0;

  private int _ID;
  public int ID {
    get {
      return this._ID;
    }
  }

  void Start( ) {
    this.gameObject.Ext();
    this._ID = nextID++;
  }
}
