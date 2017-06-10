using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public delegate void Callback(UData payload);

public class Dispatcher
{
  private Dictionary<string, List<DispatcherListener> > callbackCatagories = new Dictionary<string, List<DispatcherListener> >( );
  
  private static int eventID = 0;

  private int lastEventID = -1;

  public static void Log(UData payload)
  {
    Debug.Log(payload.Serialize());
  }

  public void Trigger(string eventName)
  {
    this.Trigger(eventName, new UData());
  }

  public void Trigger(string eventName, UData payload)
  {
    payload.Set("event", eventName);
    this.Trigger(payload);
  }

  public void Trigger(string eventName, object obj)
  {
    UData payload = new UData();
    payload.Set("event", eventName);
    payload.Set("value", obj);
    this.Trigger(payload);
  }

  public void Trigger(UData payload) {
    if(!payload.Has("eventID")) {
      payload.Set("eventID", eventID);
      eventID++;
    } else if(this.lastEventID == payload.Get<int>("eventID")){ return; }
    
    this.lastEventID = payload.Get<int>("eventID");

    string eventName = payload.Get<string>("event");
    if(eventName == null) { return; }
    payload.Set("event", eventName = eventName.ToLower());

    // if(this.bindings.ContainsKey(eventName))
    //   Trigger(this.bindings[eventName]);

    // If all
    if(eventName=="all")
    {
      // retrigger all
      foreach(KeyValuePair<string, List<DispatcherListener>> entry in this.callbackCatagories)
      {
        List<DispatcherListener> dispatcherListeners = entry.Value;
        foreach(DispatcherListener dispatcherListener in dispatcherListeners)
        {
          dispatcherListener.Trigger(payload);
        }
      }
    // Else
    }
    else
    {
      // Trigger event
      if(this.callbackCatagories.ContainsKey(eventName))
      {
        foreach(DispatcherListener dispatcherListener in this.callbackCatagories[eventName])
        {
          dispatcherListener.Trigger(payload);
        }
      }
      // Trigger "all"
      if(this.callbackCatagories.ContainsKey("all"))
      {
        foreach(DispatcherListener dispatcherListener in this.callbackCatagories["all"])
        {
          dispatcherListener.Trigger(payload);
        }
      }
    }

    // Recurse over parent callbackCatagories if '.' is present
    int lastPeriod = eventName.LastIndexOf('.');
    if(lastPeriod != -1)
    {
      eventName = eventName.Substring(0, lastPeriod);
      UData newPayload = new UData(payload);
      newPayload.Remove("eventID");
      this.Trigger(eventName, newPayload);
    }
  }

  public DispatcherListener On(string eventName, Callback callback)
  {
    return this.On(eventName, callback, new UData());
  }

  public DispatcherListener On(string eventName, Callback callback, UData payload)
  {
    eventName = eventName.ToLower();

    if (!this.callbackCatagories.ContainsKey(eventName))
    {
      this.callbackCatagories[eventName] = new List<DispatcherListener>();
    }

    DispatcherListener dispatcherListener = new DispatcherListener(callback, payload);
    this.callbackCatagories[eventName].Add(dispatcherListener);

    return dispatcherListener;
  }
/*
  private int updateDispatcherListenerReference = -1;

  ~Dispatcher()
  {
    DisableTimedTriggers();
  }

  public void EnableTimedTriggers()
  {
    Debug.Log("Timed Triggers enabled");
    if(updateDispatcherListenerReference == -1)
      updateDispatcherListenerReference = Broadcaster.On("update", ProcessTimedTriggers);
  }

  public void DisableTimedTriggers()
  {
    if(updateDispatcherListenerReference >= 0)
    {
      Broadcaster.Off(updateDispatcherListenerReference);
      updateDispatcherListenerReference = -1;
    }
  }

  private Dictionary<long, ArrayList> timedTriggers = new Dictionary<long, ArrayList>( );
  public void ProcessTimedTriggers(UData _)
  {
    long currentTime = DateTime.Now.Ticks;

    List<long> toTrigger = new List<long>();

    foreach(KeyValuePair<long, ArrayList> entry in timedTriggers)
    {
      if(currentTime > entry.Key)
        toTrigger.Add(entry.Key);
    }

    foreach(long key in toTrigger)
    {
      foreach(UData payload in timedTriggers[key])
        Trigger(payload);

      timedTriggers.Remove(key);
    }
  }

  public void TriggerIn(UData payload, float time)
  {
    TriggerIn(payload, (long)(time*1000));
  }
  public void TriggerAt(UData payload, float time)
  {
    TriggerAt(payload, (long)(time*1000));
  }
  public void TriggerIn(UData payload, long time)
  {
    TriggerAt(payload, DateTime.Now.Ticks+time);
  }
  public void TriggerAt(UData payload, long time)
  {
    if(!timedTriggers.ContainsKey(time)) timedTriggers[time] = new ArrayList();

    timedTriggers[time].Add(payload);
  }

  public DispatcherListener Bridge(Dispatcher other)
  {
    return other.On("all", this.Trigger);
  }

  public void Off(int reference)
  {
    this.callbackCatagories[eventName].Remove(reference);
  }
*/
}

public class DispatcherListener
{
  public Callback callback;
  public UData defaultPayload;

  public DispatcherListener(Callback callback, UData payload)
  {
    this.callback = callback;
    this.defaultPayload = payload;
  }

  public void Trigger(UData payload)
  {
    if(this.callback != null)
    {
      UData combinedPayload = this.defaultPayload.Clone().Merge(payload);
      this.callback(combinedPayload);
    }
  }

  public void Release()
  {
    this.callback = null;
  }
}

