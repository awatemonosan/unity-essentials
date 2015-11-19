using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectExtension : MonoBehaviour {
  static int nextID = 0;

  public bool IsHead = false;

  public Hashtable data = new Hashtable( );

  private int _ID;
  public int ID {
    get {
      return _ID;
    }
  }

  void Start( ) {
    this._ID=nextID++;
  }
}
