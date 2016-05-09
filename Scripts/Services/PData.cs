using UnityEngine;

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class PData {
  private static string CompletePath(string path){
    return Application.persistentDataPath + "/" + path;
  }
  public static bool Exists(string path){
    return File.Exists(CompletePath(path));
  }
  public static String Load(string path){
    if(!Exists(path)) return "";
    StreamReader r = new StreamReader(CompletePath(path));
    string raw = r.ReadToEnd();
    r.Close();
    return raw;
  }
  public static void Save(string path, Hashtable payload){
    Save(path, payload.Serialize());
  }
  public static void Save(string path, string raw){
    StreamWriter w = new StreamWriter(CompletePath(path));
    w.Write(raw);
    w.Close();
  }
}
