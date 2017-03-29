using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectExtensionController : MonoBehaviour {
// Variables
  static int nextID = 0;

  private Dispatcher dispatcher = new Dispatcher();
  private UModel model = new UModel( );
  private int _ID;
  public int ID
  {
    get
    {
      return _ID;
    }
  }

// MonoBehavior messages

  void Start( )
  {
    this.model.Lock("id", nextID++);
    this.model.dispatcher.On("all", this.dispatcher.Trigger);
  }

// extended behaviors

  public Dispatcher GetDispatcher()
  {
    return this.dispatcher;
  }

  public UModel GetModel()
  {
    return this.model;
  }

  public void AnimKeyframe(string evnt)
  {
    UModel payload = UModel.Parse(evnt);
    payload.Set("raw", evnt);
    this.GetDispatcher().Trigger("keyframe", payload);
  }

  public bool HasComponent<T> () where T : Component
  {
    return this.GetComponent<T>() != null;
  }

  // public bool HasComponent(string componentName)
  // {
  //   return this.gameObject.GetComponenet(componentName) != null;
  // }

// UQUERY OBJECT STATE MANAGEMENT

  public string GetState(string stateName)
  {
    return this.model.GetChild("_ukulele").GetChild("states").Get<string>(stateName);
  }

  public void SetState(string stateName, string state)
  {
    this.model.GetChild("_ukulele").GetChild("states").Set(stateName, state);
  }

// UQUERY OBJECT CLASS MANAGEMENT

  public void AddClass(string className)
  {
    this.model.GetChild("_ukulele").GetChild("classes").Set(className, true);
  }

  public void RemoveClass(string className)
  {
    this.model.GetChild("_ukulele").GetChild("classes").Remove(className);
  }

  public bool HasClass(string className)
  {
    return this.model.GetChild("_ukulele").GetChild("classes").Has(className);
  }

// UQUERY OBJECT ID MANAGEMENT

  public void AddID(string id)
  {
    UQuery.Query("." + id).RemoveID(id);
    this.model.GetChild("_ukulele").GetChild("ids").Set(id, true);
  }

  public void RemoveID(string id)
  {
    this.model.GetChild("_ukulele").GetChild("ids").Remove(id);
  }

  public bool HasID(string id)
  {
    return this.model.GetChild("_ukulele").GetChild("ids").Has(id);
  }

// Helpers
  
  public USelection Query(string queryString)
  {
    return UQuery.Query(this.gameObject, queryString);
  }

  public USelection Query(string[] queryArray)
  {
    return UQuery.Query(this.gameObject, queryArray);
  }
}
