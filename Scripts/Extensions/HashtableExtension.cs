using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class HashtableExtension {
  public static string ToString(this Hashtable that){
    return JSON.Serialize(that);
  }

  public static bool HasCycle(this Hashtable that){
    return that.HasCycle(new List<Hashtable>());
  }

  public static bool HasCycle(this Hashtable that, List<Hashtable> seen){
    if( seen.Contains(that) )
      return true;

    seen.Add(that);

    foreach( DictionaryEntry entry in that ){
      if( entry.Value.GetType() == typeof(Hashtable)){
        if( ((Hashtable) entry.Value).HasCycle(seen) )
          return true;
      }
    }

    return false;
  }

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
//Getters
  public static object Get(this Hashtable that, object key) {
    return that[key];
  }
  public static T GetAs<T>(this Hashtable that, object key) {
    return (T)that[key];
  }
  public static string GetString(this Hashtable that, object key) { return that.GetAs<string>(key); }
  public static int GetInt(this Hashtable that, object key) { return that.GetAs<int>(key); }
  public static float GetFloat(this Hashtable that, object key) { return that.GetAs<float>(key); }
  public static bool GetBool(this Hashtable that, object key) { return that.GetAs<bool>(key); }
  public static Hashtable GetChild(this Hashtable that, object key) { return that.GetAs<Hashtable>(key); }

//Setter
  public static void Set(this Hashtable that, object key, object value){
    that.Add(key,value);
  }

  public static void SetString(this Hashtable that, object key, string value) {
    that.Set(key,value);
  }
  public static void SetInt(this Hashtable that, object key, int value) {
    that.Set(key,value);
  }
  public static void SetFloat(this Hashtable that, object key, float value) {
    that.Set(key,value);
  }
  public static void SetBool(this Hashtable that, object key, bool value) {
    that.Set(key,value);
  }
  public static void SetChild(this Hashtable that, object key, Hashtable value) {
    that.Set(key,value);
  }
  public static void NewChild(this Hashtable that, object key){
    that.Set(key, new Hashtable());
  }

  public static bool HasKey(this Hashtable that, string key){
    return that.Contains(key);
  }

  public static void Load(this Hashtable that, string path){

  }

  public static void Save(this Hashtable that, string path){

  }
}