using UnityEngine;
using System.Collections;

public class MultiplayerObjectController : MonoBehaviourExtended {
  public int controlledObjectID = -1;

  public MultiplayerObjectController controlledObject {
    get {
      return Multiplayer.Get( controlledObjectID );
    }
    set {
      controlledObjectID = value.ID;
    }
  }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    
	}
}
