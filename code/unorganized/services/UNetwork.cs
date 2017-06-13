using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using Ukulele;

// using ExitGames.Client.Photon;

public class UNetwork : Singleton<UNetwork> {
  static public GameObject GetLocalPlayerController() {
    return WithInstance().localPlayerController;
  }

  static public IEnumerator WaitForWWW(WWW www) {
    yield return www;
  }

  static public string Get(string url) {
    return (new WWW(url)).text;
  }

  static public WWW PostWWW(string url, Hashtable payload) {
    Dictionary<string, string> headers = new Dictionary<string, string>();
    headers.Add( "Content-Type", "application/json" );

    string json = JSON.Serialize(payload);
    byte[] utfBytes = System.Text.Encoding.UTF8.GetBytes(json);
    WWW www = new WWW(url, utfBytes, headers);

    WithInstance().StartCoroutine(WaitForWWW(www));
    //do nothing untill json is loaded
    while (!www.isDone) { /*Do Nothing*/ }
    
    return www;
  }
  
  static public string Post(string url, Hashtable payload) {
    return PostWWW(url, payload).text;
  }

  // static public string GetString(string url) {
  //   return (new WWW(url)).text;
  // }
  // static public JSONObject GetJSON(string url) {
  //   return (new WWW(url)).json();
  // }
  // static public string PostString(string url, string str) {
  //   Dictionary<string, string> headers = new Dictionary<string, string>();
  //   headers.Add( "Content-Type", "application/json" );
  //   return ( new WWW(url, str.ToBytes(), headers) ).text;
  // }
  // static public JSONObject PostStringGetJSON(string url, string str) {
  //   return JSONObject.Create(PostString(url,str));
  // }
  // static public string PostJSON(string url, JSONObject json) {
  //   return PostString(url, json.Print());
  // }
  // static public JSONObject PostJSONGetJSON(string url, JSONObject json) {
  //   return JSONObject.Create(PostJSON(url,json));
  // }

  public GameObject localPlayerController;
  
  void OnConnectedToPhoton() {
    this.Emit("photon.init.success");
  }
  void OnFailedToConnectToPhoton (DisconnectCause cause) {
    this.Emit("photon.init.fail");
  }
  void OnCustomAuthenticationFailed (string debugMessage) {
    this.Emit("photon.auth.fail");
  }
  void OnCustomAuthenticationResponse (Dictionary< string, object > data) {
    this.Emit("photon.auth.response"); // TODO
  }
  void OnConnectedToMaster () {
    this.Emit("photon.connect.success");
  }
  void OnDisconnectedFromPhoton () {
    this.Emit("photon.disconnect");
  }
  void OnConnectionFail (DisconnectCause cause) {
    this.Emit("photon.disconnect.fail"); // TODO
  }
  void OnPhotonMaxCccuReached () {
    this.Emit("photon.disconnect.max_cccu");
  }
 
  void OnJoinedLobby () {
    this.Emit("photon.lobby.join.success");
  }
  void OnLeftLobby () {
    this.Emit("photon.lobby.left");
  }
  void OnLobbyStatisticsUpdate () {
    this.Emit("photon.lobby.update"); // UNNECESSARY?
  }
 
  void OnCreatedRoom () {
    this.Emit("photon.room.create.success");
  }
  void OnPhotonCreateRoomFailed (object[] codeAndMsg) {
    this.Emit("photon.room.create.fail"); // TODO
  }
  void OnJoinedRoom () {
    this.localPlayerController = PhotonNetwork.Instantiate("playerController", Vector3.zero, Quaternion.identity, 0);

    this.Emit("photon.room.join.success");
  }
  void OnPhotonJoinRoomFailed (object[] codeAndMsg) {
    this.Emit("photon.room.join.fail.generic"); // TODO
  }
  void OnPhotonRandomJoinFailed (object[] codeAndMsg) {
    this.Emit("photon.room.join.fail.random"); // TODO
  }
  void OnLeftRoom () {
    this.Emit("photon.room.left");
  }
  void OnPhotonCustomRoomPropertiesChanged (Hashtable propertiesThatChanged) {
    this.Emit("photon.room.properties"); // TODO
  }
  void OnPhotonPlayerConnected (PhotonPlayer newPlayer) {
    this.Emit("photon.room.player.connect"); // TODO
  }
  void OnPhotonPlayerDisconnected (PhotonPlayer otherPlayer) {
    this.Emit("photon.room.player.disconnect"); // TODO
  }
  void OnPhotonPlayerActivityChanged (PhotonPlayer otherPlayer) {
    this.Emit("photon.room.player.update"); // UNNECESSARY?
  }
  void OnPhotonPlayerPropertiesChanged (object[] playerAndUpdatedProps) {
    this.Emit("photon.room.player.properties"); // TODO
  }
  void OnMasterClientSwitched (PhotonPlayer newMasterClient) {
    this.Emit("photon.room.player.master"); // TODO
  }
  void OnPhotonInstantiate (PhotonMessageInfo info) {
    this.Emit("photon.room.object.instantiate"); // TODO
  }
  void OnOwnershipRequest (object[] viewAndPlayer) {
    this.Emit("photon.room.object.request"); // TODO
  }
  void OnOwnershipTransfered (object[] viewAndPlayers) {
    this.Emit("photon.room.object.transfered"); // UNNECESSARY?
  }
 
  void OnReceivedRoomListUpdate () {
    // this._roomList = PhotonNetwork.GetRoomList();
    this.Emit("photon.room_list"); // TODO
  }
  void OnUpdatedFriendList () {
    this.Emit("photon.friend_list"); // TODO
  }
  void OnWebRpcResponse (ExitGames.Client.Photon.OperationResponse response) {
    this.Emit("photon.webrpc"); // TODO
  }
}
