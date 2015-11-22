using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void Callback(Hashtable payload);

public class Dispatcher {
  private Dictionary<   string, ArrayList> events               = new Dictionary<   string, ArrayList>( );
  private Dictionary<      int, Hashtable> referenceToHashtable = new Dictionary<      int, Hashtable>( );
  private Dictionary<Hashtable, int      > hashtableToReference = new Dictionary<Hashtable, int      >( );
  private Dictionary<   string, string   > bindings             = new Dictionary<   string, string   >( );

  private int nextReference = 0;

  private static int eventID = 0;

  private int lastEventID = -1;

  public void Trigger(string evnt) {
    this.Trigger(evnt, new Hashtable( ));
  }
  public void Trigger(string evnt, Hashtable payload) {
    payload["event"] = evnt;
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
    string evnt = ((string)payload["event"]);
    payload["event"] = evnt = evnt.ToLower();

    if(this.bindings.ContainsKey(evnt))
      Trigger(this.bindings[evnt]);

    // If all
    if(evnt=="all") {
      // retrigger all
      foreach(KeyValuePair<string, ArrayList> entry in this.events) {
        ArrayList callbacks = entry.Value;
        foreach(Callback callback in callbacks) {
          callback(payload);
        }
      }
    // Else
    } else {
      // Trigger event
      if(this.events.ContainsKey(evnt)) {
        foreach(Callback callback in this.events[evnt]) {
          callback(payload);
        }
      }
      // Trigger every
      if(this.events.ContainsKey("every")){
        foreach(Callback callback in this.events["every"]) {
          callback(payload);
        }
      }
    }
  }

  public void Bind( string from, string to ) {
    bindings[from.ToLower()] = to.ToLower();
  }

  public void Unbind( string binding ) {
    bindings.Remove( binding.ToLower() );
  }

  public int On(string evnt, Callback callback) {
    evnt = evnt.ToLower();

    if( !this.events.ContainsKey(evnt) )
      this.events[evnt]=new ArrayList();

    ArrayList callbacks = this.events[evnt];

    if( callbacks.Contains(callback) ) return -1;
    
    Hashtable payload = new Hashtable();

    payload["callback"] = callback;
    payload["event"]    = evnt;

    referenceToHashtable[nextReference] = payload;
    hashtableToReference[payload]       = nextReference;

    callbacks.Add(callback);

    return nextReference++;
  }

  public void Off(int reference) {
    if( !referenceToHashtable.ContainsKey(reference) ) return;

    Hashtable payload = referenceToHashtable[reference];

    Callback callback = (Callback)payload["callback"];
    string evnt       = ((string)payload["event"]).ToLower();

    if( !this.events.ContainsKey(evnt) ){ Debug.Log("[ERROR][Dispatcher] Off: event doesn't exist"); return; } // THIS SHOULDN'T HAPPEN

    ArrayList callbacks = this.events[evnt];

    if( !callbacks.Contains(callback) ){ Debug.Log("[ERROR][Dispatcher] Off: callback doesn't exist"); return; } // THIS SHOULDN'T HAPPEN

    referenceToHashtable.Remove(reference);
    hashtableToReference.Remove(payload);

    callbacks.Remove(callback);
  }
}
