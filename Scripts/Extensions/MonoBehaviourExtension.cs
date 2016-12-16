using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MonoBehaviourExtension {
  public static Dispatcher GetDispatcher(this MonoBehaviour that)
  {
    that.gameObject.GetDispatcher();
  }
}
