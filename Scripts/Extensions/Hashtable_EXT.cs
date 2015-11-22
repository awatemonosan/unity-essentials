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

	public static object Get(this Hashtable that, object key) {
    return that[key];
  }
	public static string GetString(this Hashtable that, object key) { return (string)that.Get(key); }
	public static int GetInt(this Hashtable that, object key) { return (int)that.Get(key); }
	public static float GetFloat(this Hashtable that, object key) { return (float)that.Get(key); }
	public static bool GetBool(this Hashtable that, object key) { return (bool)that.Get(key); }
	public static Hashtable GetHashtable(this Hashtable that, object key) { return (Hashtable)that.Get(key); }

	public static void Set(this Hashtable that, string key, object value){
    that.Add(key,value);
  }
}