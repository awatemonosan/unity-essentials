using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
 
public class WebService {
  public static string GetString(string url){
    return (new WWW(url)).text;
  }
  public static JSONObject GetJSON(string url){
    return (new WWW(url)).json();
  }
  public static string PostString(string url, string str){
    Dictionary<string, string> headers = new Dictionary<string, string>();
    headers.Add( "Content-Type", "application/json" );
    return ( new WWW(url, str.ToBytes(), headers) ).text;
  }
  public static string PostStringGetJSON(string url, string str){
    return JSONObject.new(PostString(url,str));
  }
  public static string PostJSON(string url, JSONObject json){
    return PostString(url, json.Print());
  }
  public static JSONObject PostJSONGetJSON(string url, JSONObject json){
    return JSONObject.new(PostJSON(url,json));
  }
}