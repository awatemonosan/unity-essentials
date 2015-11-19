using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Multiplayer {
  public GameObject clientPrefab;
  public void Start(){
    Construct.Register('mp_client', clientPrefab);
    // TODO: All them broadcaster hooks
  }
}
