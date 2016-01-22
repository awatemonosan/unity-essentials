using UnityEngine;
using System;
using System.Collections;

public static class JSON {
  public static string Serialize(Hashtable data){
    easy.JSON.JsonEncode(that);
  }
  public static Hashtable Parse(string json){
    return (Hashtable)easy.JSON.JsonDecode(json);
  }
  
  public static Hashtable Load(string path){
    return Parse(PData.Load(path));
  }
  public static void Save(string path, Hashtable data){
    PData.Save(path, Serialize(data));
  }
}
