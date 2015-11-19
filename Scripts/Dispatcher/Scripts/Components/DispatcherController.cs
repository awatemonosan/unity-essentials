using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DispatcherController : MonoBehaviour {
  private Dispatcher dispatcher = new Dispatcher();
  
  public bool IsHead = false;

  public DispatcherComponent Head {
    get{
      if(this.IsHead) return this;
      DispatcherComponent up = this.Up;
      if(up == null) return this;
      DispatcherComponent head = up.Head;
      if(head == null) return null;
      return head;
    }
  }

  public DispatcherComponent Up {
    get{
      if(this.IsHead) return null;
      Transform parent = this.transform.parent;
      if(parent == null) return null;
      return parent.GetDispatcher();
    }
  }

  public void AnimKeyframe(string evnt) {
    Hashtable payload = new Hashtable( );
    payload["keyframe"] = evnt;
    this.Trigger("keyframe", payload);
  }
  public void Trigger(string evnt) {
    this.Trigger(evnt, new Hashtable( ));
  }
  public void Trigger(string evnt, Hashtable payload) {
    payload["event"] = evnt;
    this.Trigger(payload);
  }
  public void Trigger(Hashtable payload) {
    if(!payload.ContainsKey("source"))
      payload["source"] = this.gameObject;

    this.dispatcher.Trigger(payload);
  }

  public int On(string evnt, Callback callback) {
    return this.dispatcher.On(evnt, callback);
  }

  public void Off(int reference) {
    this.dispatcher.Off(reference);
  }

  public void Bind(string from, string to) {
    this.dispatcher.Bind(from, to);
  }

  public void Unbind(string binding) {
    this.dispatcher.Unbind(binding);
  }

  private void SendUp(Hashtable payload) {
    if(this.Up) this.Up.Trigger(payload);
  }

  public void Start() {
    this.On("every", this.SendUp);
  }

  public void Update() {
    this.Trigger("update");
  }
}
