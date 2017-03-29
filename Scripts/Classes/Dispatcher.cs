using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public delegate bool Callback(UModel payload);

public class Dispatcher
{
  private Dictionary<string, List<DispatcherListener> > callbackCatagories = new Dictionary<string, List<DispatcherListener> >( );
  
  private static int eventID = 0;

  private int lastEventID = -1;

  public static bool Log(UModel payload)
  {
    Debug.Log(payload.Serialize());
    return true;
  }

  public bool Trigger(string eventName)
  {
    return this.Trigger(eventName, new UModel());
  }

  public bool Trigger(string eventName, UModel payload)
  {
    payload.Set("event", eventName);
    return this.Trigger(payload);
  }

  public bool Trigger(string eventName, object obj)
  {
    UModel payload = new UModel();
    payload.Set("event", eventName);
    payload.Set("value", obj);
    return this.Trigger(payload);
  }

  public bool Trigger(UModel payload)
  {
    if(!payload.Has("eventID"))
    {
      payload.Set("eventID", eventID);
      eventID++;
    }
    else if(this.lastEventID == payload.Get<int>("eventID"))
    {
      return true;
    }
    
    this.lastEventID = payload.Get<int>("eventID");

    string eventName = payload.Get<string>("event");
    if(eventName == null) return true;
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
      UModel newPayload = new UModel(payload);
      newPayload.Remove("eventID");
      this.Trigger(eventName, newPayload);
    }
    return true;
  }

  public DispatcherListener On(string eventName, Callback callback)
  {
    return this.On(eventName, callback, new UModel());
  }

  public DispatcherListener On(string eventName, Callback callback, UModel payload)
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
  public void ProcessTimedTriggers(UModel _)
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
      foreach(UModel payload in timedTriggers[key])
        Trigger(payload);

      timedTriggers.Remove(key);
    }
  }

  public void TriggerIn(UModel payload, float time)
  {
    TriggerIn(payload, (long)(time*1000));
  }
  public void TriggerAt(UModel payload, float time)
  {
    TriggerAt(payload, (long)(time*1000));
  }
  public void TriggerIn(UModel payload, long time)
  {
    TriggerAt(payload, DateTime.Now.Ticks+time);
  }
  public void TriggerAt(UModel payload, long time)
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
  public UModel defaultPayload;

  public DispatcherListener(Callback callback, UModel payload)
  {
    this.callback = callback;
    this.defaultPayload = payload;
  }

  public bool Trigger(UModel payload)
  {
    if(this.callback != null)
    {
      UModel combinedPayload = this.defaultPayload.Clone().Merge(payload);
      return this.callback(combinedPayload);
    }
    else
    {
      return false;
    }
  }

  public void Release()
  {
    this.callback = null;
  }
}

