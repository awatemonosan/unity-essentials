using UnityEngine;
using System;
using System.Collections;

public static class PData {
  public static String Load(string path){
    StreamReader r = File.OpenText(Application.persistentDataPath + "\\" + path);
    string raw = r.ReadToEnd();
    r.close();
    return raw;
  }
  public static void Save(string path, string raw){
    StreamWriter w = File.OpenText(Application.persistentDataPath + "\\" + path);
    w.Write(raw);
    w.close();
  }
}
