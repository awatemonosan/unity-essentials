using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
 
public class WEB {
  private static string GetString(string url){
    return (new WWW(url)).text;
  }
  private static JSONObject GetJson(string url){
    return (new WWW(url)).json();
  }
  private static string PostString(string url, string str){
    Dictionary<string, string> headers = new Dictionary<string, string>();
    headers.Add( "Content-Type", "application/json" );
    return ( new WWW(url, str.ToBytes(), headers) ).text;
  }
  private static string PostJSON(string url, JSONObject json){
    return PostString(url, json.Print());
  }
}