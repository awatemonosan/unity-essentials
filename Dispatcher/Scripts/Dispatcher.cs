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

  public void Trigger(string evnt) {
    this.Trigger(evnt, new Hashtable( ));
  }
  public void Trigger(string evnt, Hashtable payload) {
    payload["event"] = evnt;
    this.Trigger(payload);
  }
  public void Trigger(Hashtable payload) {
    payload["eventID"] = eventID;
    if(payload["event"] == null) return;
    string evnt = (string)payload["event"];

    if(this.bindings.ContainsKey(evnt))
      Trigger(this.bindings[evnt]);

    if(this.events.ContainsKey(evnt)) {
      if(evnt=="all") { // when you trigger "all" it triggers ALL registered events
        foreach(KeyValuePair<string, ArrayList> entry in this.events) {
          ArrayList callbacks = entry.Value;
          foreach(Callback callback in callbacks) {
            callback(payload);
          }
        }
      } else {
        if(this.events.ContainsKey("every")){ // Every event triggers the Every event.
          foreach(Callback callback in this.events["every"]) {
            callback(payload);
          }
        }
        foreach(Callback callback in this.events[evnt]) {
          callback(payload);
        }
      }
    }
    eventID++;
  }

  public void Bind( string from, string to ) {
    bindings[from] = to;
  }

  public void Unbind( string binding ) {
    bindings.Remove( binding );
  }

  public int On(string evnt, Callback callback) {
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
    string evnt       = (string)payload["event"];

    if( !this.events.ContainsKey(evnt) ){ Debug.Log("[ERROR][Dispatcher] Off: event doesn't exist"); return; } // THIS SHOULDN'T HAPPEN

    ArrayList callbacks = this.events[evnt];

    if( !callbacks.Contains(callback) ){ Debug.Log("[ERROR][Dispatcher] Off: callback doesn't exist"); return; } // THIS SHOULDN'T HAPPEN

    referenceToHashtable.Remove(reference);
    hashtableToReference.Remove(payload);

    callbacks.Remove(callback);
  }
}
