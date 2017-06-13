using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using Ukulele;

public static class HashtableExtension {

  public static T Get<T>(this Hashtable that, object key)
  {
    object value = that.Get(key);
    if(value == null)
    {
      return default(T);
    }
    else
    {
      return (T)value;
    }
  }

  public static object Get(this Hashtable that, object key)
  {
    if(that.Has(key) == false)
    { return null; }
    return that[key];
  }

  public static void Set(this Hashtable that, object key, object value)
  {
    that[key] = value;
  }

  public static Hashtable Merge(this Hashtable that, Hashtable other, bool overwrite=false)
  {
    foreach (DictionaryEntry entry in other)
    {
      if(overwrite || !that.Has(entry.Key))
      { that[entry.Key] = entry.Value; }
    }
    return that;
  }

  public static Hashtable Copy(this Hashtable that)
  {
    return (new Hashtable()).Merge(that);
  }

  public static bool Has(this Hashtable that, object key)
  {
    return that.ContainsKey(key);
  }

  public static Hashtable GetSub(this Hashtable that, object subKey)
  // WARNING - DESTRUCTIVE
  // subKey cannot contain '.'
  // TODO: test for that
  {
    if(that.Get(subKey).GetType() != typeof(Hashtable))
    {
      that.Set(subKey, new Hashtable());
    }

    return that.Get<Hashtable>(subKey);
  }

  // public static ReturnType Reduce<ReturnType>(this Dictionary that, ReduceIterator <ReturnType>reduceIterator, ReturnType memo=default(ReturnType))
  // {
  //   foreach(KeyValuePair<object, object> kvp in that)
  //   {
  //     memo = reduceIterator(memo, kvp.Key, kvp.Value);
  //   }

  //   return memo;
  // }
  public static ReturnType Reduce<ReturnType>(this Hashtable that, ReduceIterator <ReturnType>reduceIterator, ReturnType memo=default(ReturnType))
  {
    foreach(DictionaryEntry de in that)
    {
      memo = reduceIterator(memo, de.Key, de.Value);
    }

    return memo;
  }
}

//   public static bool HasCycle(this Hashtable that){
//     return that.HasCycle(new List<Hashtable>());
//   }

//   public static bool HasCycle(this Hashtable that, List<Hashtable> seen){
//     if( seen.Contains(that) )
//       return true;

//     seen.Add(that);

//     foreach( DictionaryEntry entry in that ){
//       if( entry.Value.GetType() == typeof(Hashtable)){
//         if( ((Hashtable) entry.Value).HasCycle(seen) )
//           return true;
//       }
//     }

//     return false;
//   }

//   public static Hashtable Merge(this Hashtable that, Hashtable other)
//   {
//     foreach (DictionaryEntry entry in other)
//     {
//       that[entry.Key] = entry.Value;
//     }
//     return that;
//   }

//   public static Hashtable Copy(this Hashtable that){
//     return (new Hashtable()).Merge(that);
//   }
  
// //Getters
//   public static object Get(this Hashtable that, object key) {
//     return that[key];
//   }
//   public static T GetAs<T>(this Hashtable that, object key) {
//     if (that.Contains(key)) {
//       return (T)that[key];
//     } else {
//       return default(T);
//     }
//   }
//   public static string GetString(this Hashtable that, object key) { return that.GetAs<string>(key); }
//   public static int GetInt(this Hashtable that, object key) { return that.GetAs<int>(key); }
//   public static float GetFloat(this Hashtable that, object key) { return that.GetAs<float>(key); }
//   public static bool GetBool(this Hashtable that, object key) { return that.GetAs<bool>(key); }
//   public static Hashtable GetChild(this Hashtable that, object key) { return that.GetAs<Hashtable>(key); }

// //Setter
//   public static void Set(this Hashtable that, object key, object value){
//     that.Add(key,value);
//   }

//   public static void SetString(this Hashtable that, object key, string value) {
//     that.Set(key,value);
//   }
//   public static void SetInt(this Hashtable that, object key, int value) {
//     that.Set(key,value);
//   }
//   public static void SetFloat(this Hashtable that, object key, float value) {
//     that.Set(key,value);
//   }
//   public static void SetBool(this Hashtable that, object key, bool value) {
//     that.Set(key,value);
//   }
//   public static void SetChild(this Hashtable that, object key, Hashtable value) {
//     that.Set(key,value);
//   }
//   public static void NewChild(this Hashtable that, object key){
//     that.Set(key, new Hashtable());
//   }

//   public static bool HasKey(this Hashtable that, string key){
//     return that.Contains(key);
//   }

//   public static void Load(this Hashtable that, string path){

//   }

//   public static void Save(this Hashtable that, string path){

//   }
// }