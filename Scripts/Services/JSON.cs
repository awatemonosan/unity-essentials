using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class JSON
{
  public static string Serialize(Hashtable data)
  {
    return easy.JSON.JsonEncode(data);
  }
  public static Hashtable Parse(string json)
  {
    return (Hashtable)easy.JSON.JsonDecode(json);
  }

  public static Hashtable Load(string path)
  {
    if(!PData.Exists(path + ".json"))
      return new Hashtable();
    return Parse(PData.Load(path + ".json"));
  }
  public static void Save(string path, Hashtable data)
  {
    PData.Save(path + ".json", Serialize(data));
  }
}
