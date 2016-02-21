using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
 
public class Web {
  public static string Get(string url){
    return (new WWW(url)).text;
  }

  public static string Post(string url, Hashtable payload){
    Dictionary<string, string> headers = new Dictionary<string, string>();
    headers.Add( "Content-Type", "application/json" );
    string payload_stringified = JSON.Serialize(payload);
    return ( new WWW(url, payload_stringified.ToBytes(), headers) ).text;
  }

  // public static string GetString(string url){
  //   return (new WWW(url)).text;
  // }
  // public static JSONObject GetJSON(string url){
  //   return (new WWW(url)).json();
  // }
  // public static string PostString(string url, string str){
  //   Dictionary<string, string> headers = new Dictionary<string, string>();
  //   headers.Add( "Content-Type", "application/json" );
  //   return ( new WWW(url, str.ToBytes(), headers) ).text;
  // }
  // public static JSONObject PostStringGetJSON(string url, string str){
  //   return JSONObject.Create(PostString(url,str));
  // }
  // public static string PostJSON(string url, JSONObject json){
  //   return PostString(url, json.Print());
  // }
  // public static JSONObject PostJSONGetJSON(string url, JSONObject json){
  //   return JSONObject.Create(PostJSON(url,json));
  // }
}