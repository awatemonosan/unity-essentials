using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Hashtable_EXT {
  public static string JSON(this Hashtable that)
  {
    string s = "{";
    bool first = true;

    foreach(DictionaryEntry entry in that) {
      s = s + ""; // TODO: New Line

      if(!first){
        s = s + ", ";
      }

      first = false;

      s = s + entry.Key + ": \"" + entry.Value + "\"";
    }

    s = s + "\r}";

    return s;
    
  }

  public static void Merge(this Hashtable that, Hashtable other)
  {
    foreach (DictionaryEntry entry in other)
    {
      that[entry.Key] = entry.Value;
    }
  }
}