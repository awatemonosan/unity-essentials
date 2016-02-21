using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class API {
  private string url;
  public API(string url) {
    this.url = url;
  }

  public Hashtable Get(string path){
    return Web.Get(url+path).Parse();
  }

  public Hashtable Post(string path, Hashtable payload){
    return Web.Post(url+path, payload).Parse();;
  }
}
