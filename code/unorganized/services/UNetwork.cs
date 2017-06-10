using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

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

  static public WWW PostWWW(string url, UData payload) {
    Dictionary<string, string> headers = new Dictionary<string, string>();
    headers.Add( "Content-Type", "application/json" );

    string json = payload.Serialize();
    byte[] utfBytes = System.Text.Encoding.UTF8.GetBytes(json);
    WWW www = new WWW(url, utfBytes, headers);

    WithInstance().StartCoroutine(WaitForWWW(www));
    //do nothing untill json is loaded
    while (!www.isDone) { /*Do Nothing*/ }
    
    return www;
  }
  
  static public string Post(string url, UData payload) {
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
    this.gameObject.GetDispatcher().Trigger("photon.init.success");
  }
  void OnFailedToConnectToPhoton (DisconnectCause cause) {
    this.gameObject.GetDispatcher().Trigger("photon.init.fail");
  }
  void OnCustomAuthenticationFailed (string debugMessage) {
    this.gameObject.GetDispatcher().Trigger("photon.auth.fail");
  }
  void OnCustomAuthenticationResponse (Dictionary< string, object > data) {
    this.gameObject.GetDispatcher().Trigger("photon.auth.response"); // TODO
  }
  void OnConnectedToMaster () {
    this.gameObject.GetDispatcher().Trigger("photon.connect.success");
  }
  void OnDisconnectedFromPhoton () {
    this.gameObject.GetDispatcher().Trigger("photon.disconnect");
  }
  void OnConnectionFail (DisconnectCause cause) {
    this.gameObject.GetDispatcher().Trigger("photon.disconnect.fail"); // TODO
  }
  void OnPhotonMaxCccuReached () {
    this.gameObject.GetDispatcher().Trigger("photon.disconnect.max_cccu");
  }
 
  void OnJoinedLobby () {
    this.gameObject.GetDispatcher().Trigger("photon.lobby.join.success");
  }
  void OnLeftLobby () {
    this.gameObject.GetDispatcher().Trigger("photon.lobby.left");
  }
  void OnLobbyStatisticsUpdate () {
    this.gameObject.GetDispatcher().Trigger("photon.lobby.update"); // UNNECESSARY?
  }
 
  void OnCreatedRoom () {
    this.gameObject.GetDispatcher().Trigger("photon.room.create.success");
  }
  void OnPhotonCreateRoomFailed (object[] codeAndMsg) {
    this.gameObject.GetDispatcher().Trigger("photon.room.create.fail"); // TODO
  }
  void OnJoinedRoom () {
    this.localPlayerController = PhotonNetwork.Instantiate("playerController", Vector3.zero, Quaternion.identity, 0);

    this.gameObject.GetDispatcher().Trigger("photon.room.join.success");
  }
  void OnPhotonJoinRoomFailed (object[] codeAndMsg) {
    this.gameObject.GetDispatcher().Trigger("photon.room.join.fail.generic"); // TODO
  }
  void OnPhotonRandomJoinFailed (object[] codeAndMsg) {
    this.gameObject.GetDispatcher().Trigger("photon.room.join.fail.random"); // TODO
  }
  void OnLeftRoom () {
    this.gameObject.GetDispatcher().Trigger("photon.room.left");
  }
  void OnPhotonCustomRoomPropertiesChanged (Hashtable propertiesThatChanged) {
    this.gameObject.GetDispatcher().Trigger("photon.room.properties"); // TODO
  }
  void OnPhotonPlayerConnected (PhotonPlayer newPlayer) {
    this.gameObject.GetDispatcher().Trigger("photon.room.player.connect"); // TODO
  }
  void OnPhotonPlayerDisconnected (PhotonPlayer otherPlayer) {
    this.gameObject.GetDispatcher().Trigger("photon.room.player.disconnect"); // TODO
  }
  void OnPhotonPlayerActivityChanged (PhotonPlayer otherPlayer) {
    this.gameObject.GetDispatcher().Trigger("photon.room.player.update"); // UNNECESSARY?
  }
  void OnPhotonPlayerPropertiesChanged (object[] playerAndUpdatedProps) {
    this.gameObject.GetDispatcher().Trigger("photon.room.player.properties"); // TODO
  }
  void OnMasterClientSwitched (PhotonPlayer newMasterClient) {
    this.gameObject.GetDispatcher().Trigger("photon.room.player.master"); // TODO
  }
  void OnPhotonInstantiate (PhotonMessageInfo info) {
    this.gameObject.GetDispatcher().Trigger("photon.room.object.instantiate"); // TODO
  }
  void OnOwnershipRequest (object[] viewAndPlayer) {
    this.gameObject.GetDispatcher().Trigger("photon.room.object.request"); // TODO
  }
  void OnOwnershipTransfered (object[] viewAndPlayers) {
    this.gameObject.GetDispatcher().Trigger("photon.room.object.transfered"); // UNNECESSARY?
  }
 
  void OnReceivedRoomListUpdate () {
    // this._roomList = PhotonNetwork.GetRoomList();
    this.gameObject.GetDispatcher().Trigger("photon.room_list"); // TODO
  }
  void OnUpdatedFriendList () {
    this.gameObject.GetDispatcher().Trigger("photon.friend_list"); // TODO
  }
  void OnWebRpcResponse (ExitGames.Client.Photon.OperationResponse response) {
    this.gameObject.GetDispatcher().Trigger("photon.webrpc"); // TODO
  }
}
