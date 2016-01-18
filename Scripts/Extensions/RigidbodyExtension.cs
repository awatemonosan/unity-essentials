using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class RigidbodyExtension {
  public static RigidbodyExtensionController Ext(this Rigidbody that) {
    RigidbodyExtensionController ext = that.GetComponent<RigidbodyExtensionController>();

    if(ext == null)
      ext = that.gameObject.AddComponent<RigidbodyExtensionController>();

    return ext;
  }

}