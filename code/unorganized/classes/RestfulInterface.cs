using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ukulele;

public class RestfulInterface
{
  private string url;

  public RestfulInterface(string url)
  {
    this.url = url;
  }

  public Hashtable Get(string path)
  {
    return JSON.Parse(UNetwork.Get( url+path ));
  }

  public Hashtable Post(string path, Hashtable payload)
  {
    return JSON.Parse(UNetwork.Post( url+path , payload));
  }
}
