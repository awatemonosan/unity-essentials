using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public delegate void Callback(Hashtable payload);

public class CallbackDetail {
  public string eventName;
  public Callback callback;
  public Hashtable defaultPayload;

  public CallbackDetail(string eventName, Callback callback, Hashtable payload){
    this.eventName = eventName;
    this.callback = callback;
    this.defaultPayload = payload;
  }

  public void Trigger(Hashtable payload){
    Hashtable combinedPayload = this.defaultPayload.Copy().Merge(payload);

    callback(combinedPayload);
  }
}

public class Dispatcher {
  private Dictionary<string, Dictionary<int, CallbackDetail> > callbackCatagories = new Dictionary<string, Dictionary<int, CallbackDetail> >( );
  private Dictionary<      int, String> referenceToEventName = new Dictionary<      int, String>( );
  
  private int nextReference = 0;

  private static int eventID = 0;

  private int lastEventID = -1;

  private int updateCallbackDetailReference = -1;

  ~Dispatcher(){
    DisableTimedTriggers();
  }

  public void EnableTimedTriggers(){
    Debug.Log("Timed Triggers enabled");
    if(updateCallbackDetailReference == -1)
      updateCallbackDetailReference = Broadcaster.On("update", ProcessTimedTriggers);
  }

  public void DisableTimedTriggers(){
    if(updateCallbackDetailReference >= 0){
      Broadcaster.Off(updateCallbackDetailReference);
      updateCallbackDetailReference = -1;
    }
  }

  private Dictionary<long, ArrayList> timedTriggers = new Dictionary<long, ArrayList>( );
  public void ProcessTimedTriggers(Hashtable _){
    long currentTime = DateTime.Now.Ticks;

    List<long> toTrigger = new List<long>();

    foreach(KeyValuePair<long, ArrayList> entry in timedTriggers) {
      if(currentTime > entry.Key)
        toTrigger.Add(entry.Key);
    }

    foreach(long key in toTrigger){
      foreach(Hashtable payload in timedTriggers[key])
        Trigger(payload);

      timedTriggers.Remove(key);
    }
  }

  public void TriggerIn(Hashtable payload, float time){
    TriggerIn(payload, (long)(time*1000));
  }
  public void TriggerAt(Hashtable payload, float time){
    TriggerAt(payload, (long)(time*1000));
  }
  public void TriggerIn(Hashtable payload, long time){
    TriggerAt(payload, DateTime.Now.Ticks+time);
  }
  public void TriggerAt(Hashtable payload, long time){
    if(!timedTriggers.ContainsKey(time)) timedTriggers[time] = new ArrayList();

    timedTriggers[time].Add(payload);
  }
  public void Trigger(string eventName) {
    this.Trigger(eventName, new Hashtable());
  }
  public void Trigger(string eventName, Hashtable payload) {
    payload["event"] = eventName;
    this.Trigger(payload);
  }
  public void Trigger(string eventName, object obj) {
    Hashtable payload = new Hashtable();
    payload["event"] = eventName;
    payload["value"] = obj;
    this.Trigger(payload);
  }
  public void Trigger(Hashtable payload) {
    if(!payload.ContainsKey("eventID")) {
      payload["eventID"] = eventID;
      eventID++;
    } else if(this.lastEventID == (int)payload["eventID"]){
      return;
    }
    
    this.lastEventID = (int)payload["eventID"];

    if(payload["event"] == null) return;
    string eventName = ((string)payload["event"]);
    payload["event"] = eventName = eventName.ToLower();

    // if(this.bindings.ContainsKey(eventName))
    //   Trigger(this.bindings[eventName]);

    // If all
    if(eventName=="all") {
      // retrigger all
      foreach(KeyValuePair<string, Dictionary<int, CallbackDetail>> entry in this.callbackCatagories) {
        Dictionary<int, CallbackDetail> callbackDetails = entry.Value;
        foreach(KeyValuePair<int, CallbackDetail> entry2 in callbackDetails) {
          entry2.Value.Trigger(payload);
        }
      }
    // Else
    } else {
      // Trigger event
      if(this.callbackCatagories.ContainsKey(eventName)) {
        foreach(KeyValuePair<int, CallbackDetail> entry in this.callbackCatagories[eventName]) {
          entry.Value.Trigger(payload);
        }
      }
      // Trigger every
      if(this.callbackCatagories.ContainsKey("all")){
        foreach(KeyValuePair<int, CallbackDetail> entry in this.callbackCatagories["all"]) {
          entry.Value.Trigger(payload);
        }
      }
    }

    // Recurse over parent callbackCatagories if '.' is present
    int lastPeriod = eventName.LastIndexOf('.');
    if(lastPeriod != -1){
      eventName = eventName.Substring(0, lastPeriod);
      Trigger(eventName, payload);
    }
  }

  public int Bridge(Dispatcher other){
    return other.On("all", this.Trigger);
  }

  public int On(string eventName, Callback callback) {
    return this.On(eventName, callback, new Hashtable());
  }

  public int On(string eventName, Callback callback, Hashtable payload) {
    eventName = eventName.ToLower();

    if (!this.callbackCatagories.ContainsKey(eventName)) {
      this.callbackCatagories[eventName] = new Dictionary<int, CallbackDetail>();
    }

    CallbackDetail callbackDetail = new CallbackDetail(eventName, callback, payload);

/// TODO THIS SHITS ALL FUCKED FIGURE IT OUT
    this.referenceToEventName[nextReference] = eventName;
    this.callbackCatagories[eventName][nextReference] = callbackDetail;

    return nextReference++;
  }

  public void Off(int reference) {
    string eventName = this.referenceToEventName[reference];
    this.callbackCatagories[eventName].Remove(reference);
    this.referenceToEventName.Remove(reference);
  }
}
