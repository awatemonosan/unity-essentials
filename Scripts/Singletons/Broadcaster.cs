using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
// using Photon;

public class Broadcaster : Singleton<Broadcaster> {
  // void Awake() {Initialize();}
  void Start() {this.Trigger("start");}
  void Update() {this.Trigger("update");}
  void FixedUpdate() {this.Trigger("fixed_update");}
  void LateUpdate() {this.Trigger("late_update");}

  void OnConnectedToServer() {this.Trigger("client.connect");}
  void OnDisconnectedFromServer() {this.Trigger("client.disconnect");}
  void OnFailedToConnect() {this.Trigger("client.connect.fail");}
  void OnFailedToConnectToMasterServer() {this.Trigger("tracker.connect.fail");}
  void OnServerInitialized() {this.Trigger("server.start");}
  void OnPlayerConnected() {this.Trigger("server.player.connect");}
  void OnPlayerDisconnected() {this.Trigger("server.player.disconnect");}

  void OnPreCull() {this.Trigger("render.pre_cull");}
  void OnPreRender() {this.Trigger("render.before");}
  void OnRenderObject() {this.Trigger("render");}
  void OnPostRender() {this.Trigger("render.late");}
  void OnRenderImage(RenderTexture src, RenderTexture dest) {
    this.Trigger("render.done");
  }

  void OnGUI() {
    Event e = Event.current;
    this.Trigger("gui");
    do {
      this.Trigger("gui_"+e.type);
    } while(Event.PopEvent(e));
  }

/*
  //Old Photon hooks, Probably should refactor to its own optional singleton or maybe put in a compiler option or something idk
  public override void OnConnectedToPhoton () {
    this.Trigger("photon.connect.success");
  }
  public override void OnFailedToConnectToPhoton (DisconnectCause cause) {
    this.Trigger("photon.connect.fail");
  }
  public override void OnConnectionFail (DisconnectCause cause) {
    this.Trigger("photon.connect.fail");
  }
  public override void OnDisconnectedFromPhoton () {
    this.Trigger("photon.disconnect");
  }

  public override void OnJoinedLobby () {
    this.Trigger("photon.lobby.joined");
  }
  public override void OnLeftLobby () {
    this.Trigger("photon.lobby.left");
  }
  public override void OnLobbyStatisticsUpdate () {
    this.Trigger("photon.lobby.stat.updated");
  }

  public override void OnCreatedRoom () {
    this.Trigger("photon.room.create.success");
  }
  public override void OnPhotonCreateRoomFailed (object[] codeAndMsg) {
    this.Trigger("photon.room.create.fail");
  }
  public override void OnJoinedRoom () {
    this.Trigger("photon.room.join.success");
  }
  public override void OnPhotonJoinRoomFailed (object[] codeAndMsg) {
    this.Trigger("photon.room.join.fail");
  }
  public override void OnPhotonRandomJoinFailed (object[] codeAndMsg) {
    this.Trigger("photon.room.random_join.fail");
  }
  public override void OnLeftRoom () {
    this.Trigger("photon.room.left");
  }
  public override void OnPhotonPlayerConnected (PhotonPlayer newPlayer) {
    this.Trigger("photon.room.player.joined");
  }
  public override void OnPhotonPlayerDisconnected (PhotonPlayer otherPlayer) {
    this.Trigger("photon.room.player.left");
  }
  // public override void OnPhotonCustomRoomPropertiesChanged (Hashtable propertiesThatChanged) {
  //   this.Trigger("photon.room.property.changed");
  // }
  public override void OnPhotonPlayerPropertiesChanged (object[] playerAndUpdatedProps) {
    this.Trigger("photon.player.property.changed");
  }

  public override void OnConnectedToMaster () {
    this.Trigger("photon.master.connected");
  }
  public override void OnMasterClientSwitched (PhotonPlayer newMasterClient) {
    this.Trigger("photon.master_client.switched");
  }
  public override void OnReceivedRoomListUpdate () {
    this.Trigger("photon.room_list.updated");
  }
  public override void OnPhotonInstantiate (PhotonMessageInfo info) {
    this.Trigger("photon.player.instantiated");
  }
  public override void OnPhotonMaxCccuReached () {
    this.Trigger("photon.max_ccc_reached");
  }
  public override void OnUpdatedFriendList () {
    this.Trigger("photon.friends.updated");
  }
  public override void OnCustomAuthenticationFailed (string debugMessage) {
    this.Trigger("photon.auth.failed");
  }
  //public override  void OnWebRpcResponse (OperationResponse response) {
  //   this.Trigger("photon.web_rpc.response");
  // }
  public override void OnOwnershipRequest (object[] viewAndPlayer) {
    this.Trigger("photon.ownership_request");
  }
*/
}
