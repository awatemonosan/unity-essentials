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

  public UModel Get(string path)
  {
    return UModel.Parse(UNetwork.Get( url+path ));
  }

  public UModel Post(string path, UModel payload)
  {
    return UModel.Parse(UNetwork.Post( url+path , payload));
  }
}
