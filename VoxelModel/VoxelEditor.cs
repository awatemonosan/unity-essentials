using UnityEngine;
using System.IO;
using System.Collections;

public class VoxelEditor : MonoBehaviourExtended {
///////////////////////////////////////////////////////////////////////////////
// Inspector crap
  public  int          color     = 0;
  public  GameObject   dropBox   = null;
  private bool        _lastDraw  = false;
  private bool        _lastErase = false;
  public  bool         draw      = false;
  public  bool         erase     = false;
  public  bool         hold      = false;

  public  string       fileName  = "test.vxl";
  public  bool         save      = false;
  public  bool         load      = false;
///////////////////////////////////////////////////////////////////////////////

  byte[] GetBytes(string str) {
    byte[] bytes = new byte[str.Length * sizeof(char)];
    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
    return bytes;
  }

  string GetString(byte[] bytes){
    char[] chars = new char[bytes.Length / sizeof(char)];
    System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
    return new string(chars);
  }

	void Update () {
    VoxelModel voxelModel = this.transform.parent.GetComponent<VoxelModel>();
    if(draw&&erase){draw=_lastErase;erase=_lastDraw;}
    _lastDraw=draw;
    _lastErase=erase;

    if(dropBox){
      if(draw) {
        voxelModel.Set(transform.localPosition, dropBox, color);
        if(!hold) draw=false;
      } else if(erase) {
        voxelModel.Remove(transform.localPosition);
        if(!hold) erase=false;
      }
    }

    if(save){
      File.WriteAllBytes( fileName, GetBytes(voxelModel.ToJSON().Print()) );
      save=false;
    }
    if(load){
      voxelModel.Load( GetString(File.ReadAllBytes(fileName)) );
      load=false;
    }
	}
}
