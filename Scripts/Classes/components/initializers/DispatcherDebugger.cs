using UnityEngine;

public class DispatcherDebugger : Initializer {
  public string eventName = "all";

  void Start()
  {
    this.gameObject.GetDispatcher().On(this.eventName, Dispatcher.Log);
  }
}
