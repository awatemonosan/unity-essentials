using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectExtensionController : MonoBehaviour {
  private Dispatcher dispatcher = new Dispatcher();

  static int nextID = 0;

  public Hashtable data = new Hashtable( );

  private int _ID;
  public int ID {
    get {
      return _ID;
    }
  }

  void Start( ) {
    this._ID = nextID++;
    this.On("all", this.SendUp);
  }

  public void Update() {
    // this.Trigger("update");
  }

  public Dispatcher GetDispatcher()
  {
    return this.dispatcher;
  }

  public void AnimKeyframe(string evnt) {
    Hashtable payload = new Hashtable( );
    payload["keyframe"] = evnt;
    this.Trigger("keyframe", payload);
  }

  private void SendUp(Hashtable payload) {
    Transform upTransform = this.transform.Up();
    if(!upTransform) return;
    DispatcherController upDispatchController = upTransform.GetComponent<DispatcherController>();
    if(!upDispatchController) return;
    upDispatchController.Trigger(payload);
  }

  public string GetState(string stateName)
  {
    UQueryState stateComponent = FindStateComponent(stateName);
    if (!stateComponent) return "";
    return stateComponent.state;
  }

  public void SetState(string stateName, string state)
  {
    UQueryState stateComponent = FindStateComponent(stateName);

    if (!stateComponent)
    {
      stateComponent = this.gameObject.AddComponent<UQueryState>();
      stateComponent.name = stateName;
    }

    stateComponent.state = state;
  }

  private UQueryState FindStateComponent(string stateName)
  {
    foreach (UQueryState stateComponent in this.gameObject.GetComponents(typeof(UQueryState)))
    {
      if (stateComponent.name == name) return stateComponent;
    }
    return null;
  }

  public void AddClass(string className)
  {
    if (this.HasClass(className)) return;
    UQueryClassName classNameComponent = gameObject.AddComponent<UQueryClassName>();
    classNameComponent.className = className;
  }

  public void RemoveClass(string className)
  {
    UQueryClassName classNameComponent = this.FindClassNameComponent(className);
    if (!classNameComponent) return;
    GameObject.Destroy(classNameComponent);
  }

  public bool HasClass(string className)
  {
    UQueryClassName classNameComponent = this.FindClassNameComponent(className);
    return !!classNameComponent;
  }

  private UQueryClassName FindClassNameComponent(string stateName)
  {
    foreach (UQueryClassName classNameComponent in this.gameObject.GetComponents(typeof(UQueryClassName)))
    {
      if (classNameComponent.name == name) return classNameComponent;
    }
    return null;
  }

  public void AddID(string id)
  {
    if (this.HasID(id)) return;
    UQueryID idComponent = gameObject.AddComponent<UQueryID>();
    idComponent.id = id;
  }

  public void RemoveID(string id)
  {
    UQueryID idComponent = this.FindIDComponent(id);
    if (!idComponent) return;
    GameObject.Destroy(idComponent);
  }

  public bool HasID(string id)
  {
    UQueryID idComponent = this.FindIDComponent(id);
    return !!idComponent;
  }

  private UQueryID FindIDComponent(string id)
  {
    foreach (UQueryID idComponent in this.gameObject.GetComponents(typeof(UQueryID)))
    {
      if (idComponent.id == id) return idComponent;
    }
    return null;
  }
  
  public Selection Query(string queryString)
  {
    return new Selection(this.gameObject).Query(queryString);
  }

  public Selection Query(string[] queryArray)
  {
    return new Selection(this.gameObject).Query(queryArray);
  }

  public T WithComponent<T>() where T : Component
  {
    T component = this.gameObject.GetComponent<T>();
    if (!component)
    {
      component = this.gameObject.AddComponent<T>();
    }
    return component;
  }
}
