// Attach this to an empty GameObject to initialize this system
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
// using Photon;

public class Broadcaster : Singleton<Broadcaster> {
  // public static void Trigger(string evnt) {
  //   Broadcaster.I.Trigger(evnt);
  // }
  // public static void Trigger(string evnt, Hashtable payload) {
  //   Broadcaster.I.Trigger(evnt, payload);
  // }
  // public static void Trigger(Hashtable payload) {
  //   Broadcaster.I.Trigger(payload);
  // }
  // public static void Bind( string from, string to ) {
  //   Broadcaster.I.Bind(from, to);
  // }
  // public static void Unbind( string binding ) {
  //   Broadcaster.I.Unbind(binding);
  // }
  // public static int On(string evnt, Callback callback) {
  //   return Broadcaster.I.On(evnt, callback);
  // }
  // public static void Off(int reference) {
  //   Broadcaster.I.Off(reference);
  // }

  public bool DebugInputs = false;
  private KeyCode[][] controllerCodes = new KeyCode[][] {
    // new KeyCode[] {
    //   KeyCode.JoystickButton0,
    //   KeyCode.JoystickButton1,
    //   KeyCode.JoystickButton2,
    //   KeyCode.JoystickButton3,
    //   KeyCode.JoystickButton4,
    //   KeyCode.JoystickButton5,
    //   KeyCode.JoystickButton6,
    //   KeyCode.JoystickButton7,
    //   KeyCode.JoystickButton8,
    //   KeyCode.JoystickButton9,
    //   KeyCode.JoystickButton10,
    //   KeyCode.JoystickButton11,
    //   KeyCode.JoystickButton12,
    //   KeyCode.JoystickButton13,
    //   KeyCode.JoystickButton14,
    //   KeyCode.JoystickButton15,
    //   KeyCode.JoystickButton16,
    //   KeyCode.JoystickButton17,
    //   KeyCode.JoystickButton18,
    //   KeyCode.JoystickButton19,
    // },
    new KeyCode[] {
      KeyCode.Joystick1Button0,
      KeyCode.Joystick1Button1,
      KeyCode.Joystick1Button2,
      KeyCode.Joystick1Button3,
      KeyCode.Joystick1Button4,
      KeyCode.Joystick1Button5,
      KeyCode.Joystick1Button6,
      KeyCode.Joystick1Button7,
      KeyCode.Joystick1Button8,
      KeyCode.Joystick1Button9,
      KeyCode.Joystick1Button10,
      KeyCode.Joystick1Button11,
      KeyCode.Joystick1Button12,
      KeyCode.Joystick1Button13,
      KeyCode.Joystick1Button14,
      KeyCode.Joystick1Button15,
      KeyCode.Joystick1Button16,
      KeyCode.Joystick1Button17,
      KeyCode.Joystick1Button18,
      KeyCode.Joystick1Button19
    }, new KeyCode[] {
      KeyCode.Joystick2Button0,
      KeyCode.Joystick2Button1,
      KeyCode.Joystick2Button2,
      KeyCode.Joystick2Button3,
      KeyCode.Joystick2Button4,
      KeyCode.Joystick2Button5,
      KeyCode.Joystick2Button6,
      KeyCode.Joystick2Button7,
      KeyCode.Joystick2Button8,
      KeyCode.Joystick2Button9,
      KeyCode.Joystick2Button10,
      KeyCode.Joystick2Button11,
      KeyCode.Joystick2Button12,
      KeyCode.Joystick2Button13,
      KeyCode.Joystick2Button14,
      KeyCode.Joystick2Button15,
      KeyCode.Joystick2Button16,
      KeyCode.Joystick2Button17,
      KeyCode.Joystick2Button18,
      KeyCode.Joystick2Button19
    }, new KeyCode[] {
      KeyCode.Joystick3Button0,
      KeyCode.Joystick3Button1,
      KeyCode.Joystick3Button2,
      KeyCode.Joystick3Button3,
      KeyCode.Joystick3Button4,
      KeyCode.Joystick3Button5,
      KeyCode.Joystick3Button6,
      KeyCode.Joystick3Button7,
      KeyCode.Joystick3Button8,
      KeyCode.Joystick3Button9,
      KeyCode.Joystick3Button10,
      KeyCode.Joystick3Button11,
      KeyCode.Joystick3Button12,
      KeyCode.Joystick3Button13,
      KeyCode.Joystick3Button14,
      KeyCode.Joystick3Button15,
      KeyCode.Joystick3Button16,
      KeyCode.Joystick3Button17,
      KeyCode.Joystick3Button18,
      KeyCode.Joystick3Button19
    }, new KeyCode[] {
      KeyCode.Joystick4Button0,
      KeyCode.Joystick4Button1,
      KeyCode.Joystick4Button2,
      KeyCode.Joystick4Button3,
      KeyCode.Joystick4Button4,
      KeyCode.Joystick4Button5,
      KeyCode.Joystick4Button6,
      KeyCode.Joystick4Button7,
      KeyCode.Joystick4Button8,
      KeyCode.Joystick4Button9,
      KeyCode.Joystick4Button10,
      KeyCode.Joystick4Button11,
      KeyCode.Joystick4Button12,
      KeyCode.Joystick4Button13,
      KeyCode.Joystick4Button14,
      KeyCode.Joystick4Button15,
      KeyCode.Joystick4Button16,
      KeyCode.Joystick4Button17,
      KeyCode.Joystick4Button18,
      KeyCode.Joystick4Button19
    }, new KeyCode[] {
      KeyCode.Joystick5Button0,
      KeyCode.Joystick5Button1,
      KeyCode.Joystick5Button2,
      KeyCode.Joystick5Button3,
      KeyCode.Joystick5Button4,
      KeyCode.Joystick5Button5,
      KeyCode.Joystick5Button6,
      KeyCode.Joystick5Button7,
      KeyCode.Joystick5Button8,
      KeyCode.Joystick5Button9,
      KeyCode.Joystick5Button10,
      KeyCode.Joystick5Button11,
      KeyCode.Joystick5Button12,
      KeyCode.Joystick5Button13,
      KeyCode.Joystick5Button14,
      KeyCode.Joystick5Button15,
      KeyCode.Joystick5Button16,
      KeyCode.Joystick5Button17,
      KeyCode.Joystick5Button18,
      KeyCode.Joystick5Button19
    }, new KeyCode[] {
      KeyCode.Joystick6Button0,
      KeyCode.Joystick6Button1,
      KeyCode.Joystick6Button2,
      KeyCode.Joystick6Button3,
      KeyCode.Joystick6Button4,
      KeyCode.Joystick6Button5,
      KeyCode.Joystick6Button6,
      KeyCode.Joystick6Button7,
      KeyCode.Joystick6Button8,
      KeyCode.Joystick6Button9,
      KeyCode.Joystick6Button10,
      KeyCode.Joystick6Button11,
      KeyCode.Joystick6Button12,
      KeyCode.Joystick6Button13,
      KeyCode.Joystick6Button14,
      KeyCode.Joystick6Button15,
      KeyCode.Joystick6Button16,
      KeyCode.Joystick6Button17,
      KeyCode.Joystick6Button18,
      KeyCode.Joystick6Button19
    }, new KeyCode[] {
      KeyCode.Joystick7Button0,
      KeyCode.Joystick7Button1,
      KeyCode.Joystick7Button2,
      KeyCode.Joystick7Button3,
      KeyCode.Joystick7Button4,
      KeyCode.Joystick7Button5,
      KeyCode.Joystick7Button6,
      KeyCode.Joystick7Button7,
      KeyCode.Joystick7Button8,
      KeyCode.Joystick7Button9,
      KeyCode.Joystick7Button10,
      KeyCode.Joystick7Button11,
      KeyCode.Joystick7Button12,
      KeyCode.Joystick7Button13,
      KeyCode.Joystick7Button14,
      KeyCode.Joystick7Button15,
      KeyCode.Joystick7Button16,
      KeyCode.Joystick7Button17,
      KeyCode.Joystick7Button18,
      KeyCode.Joystick7Button19
    }, new KeyCode[] {
      KeyCode.Joystick8Button0,
      KeyCode.Joystick8Button1,
      KeyCode.Joystick8Button2,
      KeyCode.Joystick8Button3,
      KeyCode.Joystick8Button4,
      KeyCode.Joystick8Button5,
      KeyCode.Joystick8Button6,
      KeyCode.Joystick8Button7,
      KeyCode.Joystick8Button8,
      KeyCode.Joystick8Button9,
      KeyCode.Joystick8Button10,
      KeyCode.Joystick8Button11,
      KeyCode.Joystick8Button12,
      KeyCode.Joystick8Button13,
      KeyCode.Joystick8Button14,
      KeyCode.Joystick8Button15,
      KeyCode.Joystick8Button16,
      KeyCode.Joystick8Button17,
      KeyCode.Joystick8Button18,
      KeyCode.Joystick8Button19
    }
  };

  // private KeyCode[] mouseCodes = new KeyCode[]{
  //   KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Mouse2, KeyCode.Mouse3, KeyCode.Mouse4, KeyCode.Mouse5, KeyCode.Mouse6
  // };

  private KeyCode[] keyCodes = new KeyCode[] {
    KeyCode.None,KeyCode.Backspace,KeyCode.Delete,KeyCode.Tab,KeyCode.Clear,
    KeyCode.Return,KeyCode.Pause,KeyCode.Escape,KeyCode.Space,

    KeyCode.Keypad0,KeyCode.Keypad1,KeyCode.Keypad2,KeyCode.Keypad3,
    KeyCode.Keypad4,KeyCode.Keypad5,KeyCode.Keypad6,KeyCode.Keypad7,
    KeyCode.Keypad8,KeyCode.Keypad9,KeyCode.KeypadPeriod,KeyCode.KeypadDivide,
    KeyCode.KeypadMultiply,KeyCode.KeypadMinus,KeyCode.KeypadPlus,
    KeyCode.KeypadEnter,KeyCode.KeypadEquals,

    KeyCode.UpArrow,KeyCode.DownArrow,KeyCode.RightArrow,KeyCode.LeftArrow,

    KeyCode.Insert,KeyCode.Home,KeyCode.End,KeyCode.PageUp,KeyCode.PageDown,

    KeyCode.F1,KeyCode.F2,KeyCode.F3,KeyCode.F4,KeyCode.F5,KeyCode.F6,
    KeyCode.F7,KeyCode.F8,KeyCode.F9,KeyCode.F10,KeyCode.F11,KeyCode.F12,
    KeyCode.F13,KeyCode.F14,KeyCode.F15,

    KeyCode.Alpha0,KeyCode.Alpha1,KeyCode.Alpha2,KeyCode.Alpha3,KeyCode.Alpha4,
    KeyCode.Alpha5,KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9,

    KeyCode.Exclaim,KeyCode.DoubleQuote,KeyCode.Hash,KeyCode.Dollar,
    KeyCode.Ampersand,KeyCode.Quote,KeyCode.LeftParen,KeyCode.RightParen,
    KeyCode.Asterisk,KeyCode.Plus,KeyCode.Comma,KeyCode.Minus,KeyCode.Period,
    KeyCode.Slash,KeyCode.Colon,KeyCode.Semicolon,KeyCode.Less,KeyCode.Equals,
    KeyCode.Greater,KeyCode.Question,KeyCode.At,KeyCode.LeftBracket,
    KeyCode.Backslash,KeyCode.RightBracket,KeyCode.Caret,KeyCode.Underscore,
    KeyCode.BackQuote,

    KeyCode.A,KeyCode.B,KeyCode.C,KeyCode.D,KeyCode.E,KeyCode.F,KeyCode.G,
    KeyCode.H,KeyCode.I,KeyCode.J,KeyCode.K,KeyCode.L,KeyCode.M,KeyCode.N,
    KeyCode.O,KeyCode.P,KeyCode.Q,KeyCode.R,KeyCode.S,KeyCode.T,KeyCode.U,
    KeyCode.V,KeyCode.W,KeyCode.X,KeyCode.Y,KeyCode.Z,

    KeyCode.Numlock,KeyCode.CapsLock,KeyCode.ScrollLock,KeyCode.RightShift,
    KeyCode.LeftShift,KeyCode.RightControl,KeyCode.LeftControl,
    KeyCode.RightAlt,KeyCode.LeftAlt,KeyCode.LeftCommand,KeyCode.LeftApple,
    KeyCode.LeftWindows,KeyCode.RightCommand,KeyCode.RightApple,
    KeyCode.RightWindows,KeyCode.AltGr,

    KeyCode.Help,KeyCode.Print,KeyCode.SysReq,KeyCode.Break,KeyCode.Menu
  };

  void Awake() {Initialize();}
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
  void OnPostRender() {this.Trigger("render.late");}
  void OnPreCull() {this.Trigger("render.pre_cull");}
  void OnPreRender() {this.Trigger("render.before");}
  void OnRenderObject() {this.Trigger("render");}
  
  
  // Photon hooks
  // public override void OnConnectedToPhoton () {
  //   this.Trigger("photon.connect.success");
  // }
  // public override void OnFailedToConnectToPhoton (DisconnectCause cause) {
  //   this.Trigger("photon.connect.fail");
  // }
  // public override void OnConnectionFail (DisconnectCause cause) {
  //   this.Trigger("photon.connect.fail");
  // }
  // public override void OnDisconnectedFromPhoton () {
  //   this.Trigger("photon.disconnect");
  // }

  // public override void OnJoinedLobby () {
  //   this.Trigger("photon.lobby.joined");
  // }
  // public override void OnLeftLobby () {
  //   this.Trigger("photon.lobby.left");
  // }
  // public override void OnLobbyStatisticsUpdate () {
  //   this.Trigger("photon.lobby.stat.updated");
  // }

  // public override void OnCreatedRoom () {
  //   this.Trigger("photon.room.create.success");
  // }
  // public override void OnPhotonCreateRoomFailed (object[] codeAndMsg) {
  //   this.Trigger("photon.room.create.fail");
  // }
  // public override void OnJoinedRoom () {
  //   this.Trigger("photon.room.join.success");
  // }
  // public override void OnPhotonJoinRoomFailed (object[] codeAndMsg) {
  //   this.Trigger("photon.room.join.fail");
  // }
  // public override void OnPhotonRandomJoinFailed (object[] codeAndMsg) {
  //   this.Trigger("photon.room.random_join.fail");
  // }
  // public override void OnLeftRoom () {
  //   this.Trigger("photon.room.left");
  // }
  // public override void OnPhotonPlayerConnected (PhotonPlayer newPlayer) {
  //   this.Trigger("photon.room.player.joined");
  // }
  // public override void OnPhotonPlayerDisconnected (PhotonPlayer otherPlayer) {
  //   this.Trigger("photon.room.player.left");
  // }
  // // public override void OnPhotonCustomRoomPropertiesChanged (Hashtable propertiesThatChanged) {
  // //   this.Trigger("photon.room.property.changed");
  // // }
  // public override void OnPhotonPlayerPropertiesChanged (object[] playerAndUpdatedProps) {
  //   this.Trigger("photon.player.property.changed");
  // }

  // public override void OnConnectedToMaster () {
  //   this.Trigger("photon.master.connected");
  // }
  // public override void OnMasterClientSwitched (PhotonPlayer newMasterClient) {
  //   this.Trigger("photon.master_client.switched");
  // }
  // public override void OnReceivedRoomListUpdate () {
  //   this.Trigger("photon.room_list.updated");
  // }
  // public override void OnPhotonInstantiate (PhotonMessageInfo info) {
  //   this.Trigger("photon.player.instantiated");
  // }
  // public override void OnPhotonMaxCccuReached () {
  //   this.Trigger("photon.max_ccc_reached");
  // }
  // public override void OnUpdatedFriendList () {
  //   this.Trigger("photon.friends.updated");
  // }
  // public override void OnCustomAuthenticationFailed (string debugMessage) {
  //   this.Trigger("photon.auth.failed");
  // }
  // //public override  void OnWebRpcResponse (OperationResponse response) {
  // //   this.Trigger("photon.web_rpc.response");
  // // }
  // public override void OnOwnershipRequest (object[] viewAndPlayer) {
  //   this.Trigger("photon.ownership_request");
  // }
  
  void OnGUI() {
    Event e = Event.current;
    this.Trigger("gui");
    do {
      this.Trigger("gui."+e.type);
    } while(Event.PopEvent(e));
  }

  void OnRenderImage(RenderTexture src, RenderTexture dest) {
    Hashtable payload = new Hashtable();

    payload["src"] = src;
    payload["dest"] = dest;

    this.Trigger("render.done", payload);
  }

  void Initialize() {
    // this.On("update", UpdateInputs);
  }

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////INPUTS/////////////////////////////////////////////////////////////////////////////////////////////////////////////
  private string GenerateName(string[] names){ return String.Join(".", names).ToLower(); }
  private Dictionary<string,float[]> inputStates = new Dictionary<string,float[]>();

  private void DefaultInput(string input, float def=0){
    if( !inputStates.ContainsKey(input) )
      inputStates[input] = new float[]{def, def};
  }
  private float GetState(string input) {
    DefaultInput(input);
    return inputStates[input][0];
  }
  private float GetDelta(string input) {
    DefaultInput(input);
    return inputStates[input][1];
  }

  private float GetBinaryInputState(string input) {
    DefaultInput(input, 0);

    float newState = inputStates[input][0];
    float state    = inputStates[input][1];

    newState = newState > 0 ? 0 : 1;
    if(state > 0 && newState > 0)
      newState = 2;
    if(state <= 0 && newState <= 0)
      newState = -1;

    return newState;
  }

  private float GetAnalogInputState(string input) {
    DefaultInput(input, 0);

    return Mathf.Max(-1, Mathf.Min(1, inputStates[input][0]));
  }

  private void UpdateInput(string input, float newState) {
    DefaultInput(input, 0);
  
    inputStates[input][1] = inputStates[input][0];
    inputStates[input][0] = newState;
  }

  private void UpdateInputs(Hashtable payload) {
    UpdateKeyboard();
    UpdateControllers();
    UpdateMouse();
  }

  private Hashtable NewInputPayload(string input){
    Hashtable payload = new Hashtable();

    payload["input"] = input;
    payload["state"] = GetState(input);
    payload["delta"] = GetDelta(input);

    return payload;
  }

  private void UpdateBinaryInput(string input, float state){
    UpdateInput(input, state>0?1:0);
  }

  private void UpdateKeyboard() {
    // input.keyboard.[KEY_NAME].up
    // input.keyboard.[KEY_NAME].down
    // input.keyboard.[KEY_NAME].held
    foreach(KeyCode keyCode in keyCodes) {
      string input = keyCode.ToString();
      float state = Input.GetKey(keyCode)?1f:0f;

      UpdateBinaryInput(input, state);

      Hashtable payload = NewInputPayload(input);
      payload["event"] = "input.keyboard";
      this.Trigger(payload);
    }
  }

  private void UpdateControllers() {
    //input.controller.#.button.#.pressed
    //input.controller.#.button.#.held
    //input.controller.#.button.#.released
    for(int controllerID = 1; controllerID < controllerCodes.Length; controllerID ++){
      KeyCode[] controllerCodeGroup = controllerCodes[controllerID];
      for(int buttonID = 0; buttonID<controllerCodeGroup.Length; buttonID ++){
        string input = "Controller"+ controllerID +"Button"+ buttonID;
        float state = Input.GetKey(controllerCodeGroup[buttonID])?1:0;

        UpdateBinaryInput(input, state);

        Hashtable payload = NewInputPayload(input);
        payload["event"] = "input.controller";
        payload["controller"] = controllerID;
        payload["button"] = buttonID;

        this.Trigger(payload);
      }
    }
    //input.controller.#.axis.#.move
    for(int controllerID=1; controllerID<=11; controllerID++) {
      for(int axisID=0; axisID<=19; axisID++) {
        string input = "axis."+ axisID;
        string axisName = controllerID +"-"+ axisID;
        float state = Input.GetAxis(axisName);

        UpdateInput(input, state);

        Hashtable payload = NewInputPayload(input);
        payload["event"] = "input.controller";
        payload["controller"] = controllerID;
        payload["axis"] = axisID;

        this.Trigger(payload);
      }
    }
  }

  private void UpdateMouse() {
    // Mouse
    // Differ("MouseX", Input.mousePosition.x, "Mouse", true);
    // Differ("MouseY", Input.mousePosition.y, "Mouse", true);
  }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}
