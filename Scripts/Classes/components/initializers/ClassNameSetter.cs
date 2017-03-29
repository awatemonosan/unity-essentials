using UnityEngine;
using System.Collections;

public class ClassNameSetter : Initializer {
  public string className = "";

  void Awake()
  {
    this.gameObject.AddClass(className);
  }
}
