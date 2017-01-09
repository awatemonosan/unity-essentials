using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ListExtension
{
  public static T Sample<T>(this List<T> that
  {
    return that[Mathf.FloorToInt(Random.value * that.Count)];
  }
}