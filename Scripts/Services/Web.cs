using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
 
public class Web {
  public static IEnumerator WaitForWWW(WWW www)
  {
    yield return www;
  }

  public static string Get(string url){
    return (new WWW(url)).text;
  }

  public static WWW PostWWW(string url, Hashtable payload){
    Dictionary<string, string> headers = new Dictionary<string, string>();
    headers.Add( "Content-Type", "application/json" );

    string json = JSON.Serialize(payload);
    byte[] utfBytes = System.Text.Encoding.UTF8.GetBytes(json);
    WWW www = new WWW(url, utfBytes, headers);

    DispatcherController temp = new GameObject().GetDispatcher(); // LOL
    temp.StartCoroutine(WaitForWWW(www));
    //do nothing untill json is loaded
    while (!www.isDone) { /*Do Nothing*/ }
    
    GameObject.Destroy(temp.gameObject); // LOOOOOL
    return www;
  }
  public static string Post(string url, Hashtable payload){
    return PostWWW(url, payload).text;
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