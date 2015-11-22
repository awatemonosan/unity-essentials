using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerClientController : MonoBehaviourExtended {
  public bool isClient = false;

  void Start(){
    BroadcastService.On("input", this.Input);
  }
  
  private void Input(Hashtable payload) {
    MultiplayerObjectController controlledObject = this.GetComponent<MultiplayerObjectController>().controlledObject;
    if(controlledObject) 
      controlledObject.Trigger(payload);
    
    if(isClient){
      //save inputs
    } else {
      //interpolate network inputs
    }
  } 
}
