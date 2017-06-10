using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RestfulInterface
{
  private string url;

  public RestfulInterface(string url)
  {
    this.url = url;
  }

  public UData Get(string path)
  {
    return UData.Parse(UNetwork.Get( url+path ));
  }

  public UData Post(string path, UData payload)
  {
    return UData.Parse(UNetwork.Post( url+path , payload));
  }
}
