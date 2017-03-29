using UnityEngine;

public class Ukulele : Singleton<Ukulele>
{
  void Awake ()
  {
    UInput.WithInstance();
    UObjectClickActivatorEngine.WithInstance();
  }

  void Start() { this.GetDispatcher().Trigger("start"); }
  void Update() { this.GetDispatcher().Trigger("update"); }

  void FixedUpdate() { this.GetDispatcher().Trigger("fixedUpdate"); }
  void LateUpdate() { this.GetDispatcher().Trigger("lateUpdate"); }

  void OnConnectedToServer() { this.GetDispatcher().Trigger("client.connect"); }
  void OnDisconnectedFromServer() { this.GetDispatcher().Trigger("client.disconnect"); }
  void OnFailedToConnect() { this.GetDispatcher().Trigger("client.connectFail"); }
  void OnFailedToConnectToMasterServer() { this.GetDispatcher().Trigger("tracker.connectFail"); }
  void OnServerInitialized() { this.GetDispatcher().Trigger("server.start"); }
  void OnPlayerConnected() { this.GetDispatcher().Trigger("server.playerConnect"); }
  void OnPlayerDisconnected() { this.GetDispatcher().Trigger("server.playerDisconnect"); }

  void OnPreCull() { this.GetDispatcher().Trigger("render.pre_cull"); }
  void OnPreRender() { this.GetDispatcher().Trigger("beforeRender"); }
  void OnRenderObject() { this.GetDispatcher().Trigger("render"); }
  void OnPostRender() { this.GetDispatcher().Trigger("afterRender"); }
  void OnRenderImage(RenderTexture src, RenderTexture dest) {
    this.GetDispatcher().Trigger("doneRender");
  }

  void OnGUI()
  {
    Event e = Event.current;
    this.GetDispatcher().Trigger("gui");
    do
    {
      this.GetDispatcher().Trigger("gui"+e.type);
    } while( Event.PopEvent(e) );
  }
}
