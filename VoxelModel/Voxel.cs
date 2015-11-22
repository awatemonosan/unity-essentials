using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Voxel : MonoBehaviourExtended {
  private Color      _color;
  private PhysDesc   _physDesc;
  private Vector3    _position;

  public void OnEnable() {
    gameObject.hideFlags = HideFlags.HideInHierarchy;
  }

  public Vector3 position {
    get {
      return _position.Clone();
    }
    set {
      _position.x = Mathf.Floor(value.x);
      _position.y = Mathf.Floor(value.y);
      _position.z = Mathf.Floor(value.z);

      //TODO: SET GAMEOBJECT POSITION
    }
  }

  public Color color {
    get {
      return _color.Clone();
    }
    set {
      if(_color!=value){
        _color=value;
        this.SetVertexColor(value);
      }
    }
  }
}
