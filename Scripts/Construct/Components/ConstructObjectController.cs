using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConstructObjectController : MonoBehaviour {
  static int nextID = 0;

  private int _ID;
  public int ID {
    get {
      return _ID;
    }
  }

  void Start( ) {
    this.Ext();
    this._ID=nextID++;
  }
}
