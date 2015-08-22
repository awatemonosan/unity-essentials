using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DispatcherComponent : MonoBehaviour {
  public bool Head = false;

  private Dispatcher dispatcher = new Dispatcher();

  public void Trigger(string evnt) {
    Hashtable payload = new Hashtable();
    payload["source"] = this.gameObject;

    dispatcher.Trigger(evnt, payload);
  }
  public void Trigger(Hashtable payload) {
    if(!payload.ContainsKey("source"))
      payload["source"] = this.gameObject;

    dispatcher.Trigger(payload);
  }
  public void Trigger(string evnt, Hashtable payload) {
    if(!payload.ContainsKey("source"))
      payload["source"] = this.gameObject;

    dispatcher.Trigger(evnt, payload);
  }

  public int On(string evnt, Callback callback) {
    return dispatcher.On(evnt, callback);
  }

  public void Off(int reference) {
    dispatcher.Off(reference);
  }

  public void Bind(string from, string to) {
    dispatcher.Bind(from, to);
  }

  public void Unbind(string binding) {
    dispatcher.Unbind(binding);
  }

  private void SendUp(Hashtable payload) {
    Transform head = this.transform.parent;
    while(head != null){
      if(head.GetDispatcher().Head == true)
        head.Trigger(payload);

      head = head.parent;
    }
  }

  public void Start() {
    dispatcher.On("Every", this.SendUp);
  }

  public void Update() {
    dispatcher.Trigger("Update");
  }
}
