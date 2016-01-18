using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class API {
  private string url;
  public API(string url) {
    this.url = url;
  }

  public Hashtable Get(string path){
    return WebService.Get(url+path).Parse();
  }

  public Hashtable Post(string path, Hashtable payload){
    return WebService.Post(url+path, payload).Parse();;
  }
}
