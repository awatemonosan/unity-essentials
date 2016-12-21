using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
// using Photon;

public class Broadcaster : Singleton<Broadcaster> 
{
  // void Awake()
  {
    Initialize();
  }
  void Start() {
    this.GetDispatcher().Trigger("start");
  }
  void Update() 
  {
    this.GetDispatcher().Trigger("update");
  }
  void FixedUpdate() 
  {
    this.GetDispatcher().Trigger("fixed_update");
  }
  void LateUpdate() 
  {
    this.GetDispatcher().Trigger("late_update");
  }

  void OnConnectedToServer() 
  {
    this.GetDispatcher().Trigger("client.connect");
  }
  void OnDisconnectedFromServer() 
  {
    this.GetDispatcher().Trigger("client.disconnect");
  }
  void OnFailedToConnect()
  {
    this.GetDispatcher().Trigger("client.connect.fail");
  }
  void OnFailedToConnectToMasterServer()
  {
    this.GetDispatcher().Trigger("tracker.connect.fail");
  }
  void OnServerInitialized()
  {
    this.GetDispatcher().Trigger("server.start");
  }
  void OnPlayerConnected() 
  {
    this.GetDispatcher().Trigger("server.player.connect");
  }
  void OnPlayerDisconnected()
  {
    this.GetDispatcher().Trigger("server.player.disconnect");
  }

  void OnPreCull()
  {
    this.GetDispatcher().Trigger("render.pre_cull");
  }
  void OnPreRender() 
  {
    this.GetDispatcher().Trigger("render.before");
  }
  void OnRenderObject() 
  {
    this.GetDispatcher().Trigger("render");
  }
  void OnPostRender() 
  {
    this.GetDispatcher().Trigger("render.late");
  }
  void OnRenderImage(RenderTexture src, RenderTexture dest)
  {
    this.GetDispatcher().Trigger("render.done");
  }

  void OnGUI() 
  {
    Event e = Event.current;
    this.GetDispatcher().Trigger("gui");
    do
    {
      this.GetDispatcher().Trigger("gui_"+e.type);
    } 
    while(Event.PopEvent(e));
  }

/*
  //Old Photon hooks, Probably should refactor to its own optional singleton or maybe put in a compiler option or something idk
  public override void OnConnectedToPhoton () {
    this.GetDispatcher().Trigger("photon.connect.success");
  }
  public override void OnFailedToConnectToPhoton (DisconnectCause cause) {
    this.GetDispatcher().Trigger("photon.connect.fail");
  }
  public override void OnConnectionFail (DisconnectCause cause) {
    this.GetDispatcher().Trigger("photon.connect.fail");
  }
  public override void OnDisconnectedFromPhoton () {
    this.GetDispatcher().Trigger("photon.disconnect");
  }

  public override void OnJoinedLobby () {
    this.GetDispatcher().Trigger("photon.lobby.joined");
  }
  public override void OnLeftLobby () {
    this.GetDispatcher().Trigger("photon.lobby.left");
  }
  public override void OnLobbyStatisticsUpdate () {
    this.GetDispatcher().Trigger("photon.lobby.stat.updated");
  }

  public override void OnCreatedRoom () {
    this.GetDispatcher().Trigger("photon.room.create.success");
  }
  public override void OnPhotonCreateRoomFailed (object[] codeAndMsg) {
    this.GetDispatcher().Trigger("photon.room.create.fail");
  }
  public override void OnJoinedRoom () {
    this.GetDispatcher().Trigger("photon.room.join.success");
  }
  public override void OnPhotonJoinRoomFailed (object[] codeAndMsg) {
    this.GetDispatcher().Trigger("photon.room.join.fail");
  }
  public override void OnPhotonRandomJoinFailed (object[] codeAndMsg) {
    this.GetDispatcher().Trigger("photon.room.random_join.fail");
  }
  public override void OnLeftRoom () {
    this.GetDispatcher().Trigger("photon.room.left");
  }
  public override void OnPhotonPlayerConnected (PhotonPlayer newPlayer) {
    this.GetDispatcher().Trigger("photon.room.player.joined");
  }
  public override void OnPhotonPlayerDisconnected (PhotonPlayer otherPlayer) {
    this.GetDispatcher().Trigger("photon.room.player.left");
  }
  // public override void OnPhotonCustomRoomPropertiesChanged (Hashtable propertiesThatChanged) {
  //   this.GetDispatcher().Trigger("photon.room.property.changed");
  // }
  public override void OnPhotonPlayerPropertiesChanged (object[] playerAndUpdatedProps) {
    this.GetDispatcher().Trigger("photon.player.property.changed");
  }

  public override void OnConnectedToMaster () {
    this.GetDispatcher().Trigger("photon.master.connected");
  }
  public override void OnMasterClientSwitched (PhotonPlayer newMasterClient) {
    this.GetDispatcher().Trigger("photon.master_client.switched");
  }
  public override void OnReceivedRoomListUpdate () {
    this.GetDispatcher().Trigger("photon.room_list.updated");
  }
  public override void OnPhotonInstantiate (PhotonMessageInfo info) {
    this.GetDispatcher().Trigger("photon.player.instantiated");
  }
  public override void OnPhotonMaxCccuReached () {
    this.GetDispatcher().Trigger("photon.max_ccc_reached");
  }
  public override void OnUpdatedFriendList () {
    this.GetDispatcher().Trigger("photon.friends.updated");
  }
  public override void OnCustomAuthenticationFailed (string debugMessage) {
    this.GetDispatcher().Trigger("photon.auth.failed");
  }
  //public override  void OnWebRpcResponse (OperationResponse response) {
  //   this.GetDispatcher().Trigger("photon.web_rpc.response");
  // }
  public override void OnOwnershipRequest (object[] viewAndPlayer) {
    this.GetDispatcher().Trigger("photon.ownership_request");
  }
*/
}
