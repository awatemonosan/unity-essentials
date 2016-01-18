using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DispatcherController : MonoBehaviour {
  private Dispatcher dispatcher = new Dispatcher();

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
    DispatcherController up = this.transform.Up().GetComponent<DispatcherController>();;
    if(up) up.Trigger(payload);
  }

  public void Start() {
    this.On("every", this.SendUp);
  }
}
