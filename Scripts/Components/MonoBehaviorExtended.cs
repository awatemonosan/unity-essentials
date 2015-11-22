using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon;

public class MonoBehaviourExtended : Photon.PunBehaviour {
  public void IgnoreCollision(GameObject other, bool ignore=true) {
    this.gameObject.IgnoreCollision(other, ignore);
  }
  public T Up<T>() {
    return this.gameObject.Up().GetComponent<T>();
  }
  public T Up<T>(int count) {
    return this.gameObject.Up(count).GetComponent<T>();
  }
  public bool IsHead() {
    return this.gameObject.IsHead();
  }
  public T Head<T>() {
    return this.gameObject.Head().GetComponent<T>();
  }
  public bool IsTop() {
    return this.gameObject.IsTop();
  }
  public T Top<T>() {
    return this.gameObject.Top().GetComponent<T>();
  }

  public GameObjectExtension Ext() {
    return this.gameObject.Ext();
  }
  public Hashtable Data() {
    return this.gameObject.Data();
  }
}
