using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon;

public class PhotonService {
  public static void Start() {
    // BroadcastService.On("photon.connected", this.OnConnected);
    PhotonNetwork.automaticallySyncScene = true;
    PhotonNetwork.ConnectUsingSettings("0.1");
    BroadcastService.On("photon.lobby.joined", this.OnLobbyJoined);
  }

  public static void OnLobbyJoined() {
    // PhotonNetwork.CreateRoom("master");
  }
}

  // public bool isServer = true;

  // private static Dictionary<int, GameObject> createdObjects = new Dictionary<int, GameObject>();
  // private nextID = 0;

  // public static void Start() {

  //   PhotonNetwork.automaticallySyncScene = true;
  //   PhotonNetwork.ConnectUsingSettings("0.1");
  //   PhotonNetwork.CreateRoom("master");
  // }

  // void OnGUI()
  // {
  //   GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
  // }

  // public static GameObject Create (string prefabName) {
  //   if(isServer) {
  //     GameObject newObj = Construct.Create(prefabName);
  //     MultiplayerObjectController mpc = newObj.AddComponent<MultiplayerObjectController>(nextID);
  //     Photon
  //     nextID++;

  //     return newObj;
  //   } else {
  //     return null;
  //   }
  // }

  // public static GameObject Get(int ID) {
  //   GameObject obj = null;

  //   if(createdObjects.ContainsKey(ID))
  //     obj = createdObjects[ID];

  //   return obj;
  // }
}
