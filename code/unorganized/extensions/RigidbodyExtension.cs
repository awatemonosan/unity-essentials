using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ukulele;

public static class RigidbodyExtension {
  public static bool IsOnGround(this Rigidbody that)
  {
    return that.WithComponent<UkuleleController>().IsOnGround();
  }

  public static bool IsTouching(this Rigidbody that, object other)
  {
    return that.WithComponent<UkuleleController>().IsTouching(other);
  }
}
