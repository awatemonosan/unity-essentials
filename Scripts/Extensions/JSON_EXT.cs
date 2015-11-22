using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public static class JSON_EXT {
  public static JSONObject json(this WWW that) {
    return new JSONObject(that.text);
  }
}
