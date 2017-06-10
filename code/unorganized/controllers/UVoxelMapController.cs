using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UVoxelMapController {
  UData model, voxelMap, chunkMap;
  public Dispatcher dispatcher = new Dispatcher();
  // emits:
  // voxel_updated
  // chunk_created
  // chunk_destroyed

  public UVoxelMapController(UData model) {
    this.model = model;

    this.model.Default<int>("chunkSize", 5);

    this.model.Default<int>("xMin", 0);
    this.model.Default<int>("yMin", 0);
    this.model.Default<int>("zMin", 0);

    this.model.Default<int>("xMax", 0);
    this.model.Default<int>("yMax", 0);
    this.model.Default<int>("zMax", 0);

    this.voxelMap = this.model.GetChild("voxelMap");
    this.chunkMap = this.model.GetChild("chunkMap");
  }
  
  public int GetChunkSize() { return this.model.Get<int>("chunkSize"); }

  public Vector3 GetLowerBound() { return new Vector3(this.GetXMin(), this.GetYMin(), this.GetZMin()); }
  public Vector3 GetUpperBound() { return new Vector3(this.GetXMin(), this.GetYMin(), this.GetZMin()); }

  public int GetXMin() { return this.model.Get<int>("xMin"); }
  public int GetXMax() { return this.model.Get<int>("xMax"); }

  public int GetYMin() { return this.model.Get<int>("yMin"); }
  public int GetYMax() { return this.model.Get<int>("yMax"); }

  public int GetZMin() { return this.model.Get<int>("zMin"); }
  public int GetZMax() { return this.model.Get<int>("zMax"); }

  public bool HasAt(int x, int y, int z) {
    if (!this.voxelMap.Has(x)) { return false; }
    if (!this.voxelMap.GetChild(x).Has(y)) { return false; }
    if (!this.voxelMap.GetChild(x).GetChild(y).Has(z)) { return false; }
    return true;
  }

  public void SetAt(UData voxelData, int x, int y, int z) {
    this.voxelMap.GetChild(x).GetChild(y).GetChild(z).Merge(voxelData);

    if(x < this.GetXMin()) { this.model.Set("xMin", x); }
    if(x > this.GetXMin()) { this.model.Set("xMax", x); }

    if(y < this.GetYMin()) { this.model.Set("yMin", y); }
    if(y > this.GetYMin()) { this.model.Set("yMax", y); }

    if(z < this.GetZMin()) { this.model.Set("zMin", z); }
    if(z > this.GetZMin()) { this.model.Set("zMax", z); }

    int xChunk = this.ToChunk(x);
    int yChunk = this.ToChunk(y);
    int zChunk = this.ToChunk(z);

    bool chunkExists = true;
    if (!this.chunkMap.Has(xChunk)) { chunkExists = false; }
    if (!this.chunkMap.GetChild(xChunk).Has(yChunk)) { chunkExists = false; }
    if (!this.chunkMap.GetChild(xChunk).GetChild(yChunk).Has(zChunk)) { chunkExists = false; }
    if (!chunkExists) {
      this.chunkMap.GetChild(xChunk).GetChild(yChunk).Set(zChunk, true);

      UData chunkEventData = new UData();
        chunkEventData.Set("x", xChunk);
        chunkEventData.Set("y", yChunk);
        chunkEventData.Set("z", zChunk);

      this.dispatcher.Trigger("chunk_created", chunkEventData);
    }
  }

  private UData CreateVoxelAt(int x, int y, int z){
    UData voxelData = new UData();
      voxelData.Set("fill", 0f);

    this.SetAt(voxelData, x, y, z);
    return voxelData;
  }

  public UData WithAt(int x, int y, int z) {
    if(!this.HasAt(x, y, z)) { return CreateVoxelAt(x, y, z); }
    return this.voxelMap.GetChild(x).GetChild(y).GetChild(z);
  }

  public UData GetAt(int x, int y, int z) {
    if(!this.HasAt(x, y, z)) { return new UData(); }
    return this.voxelMap.GetChild(x).GetChild(y).GetChild(z).Clone();
  }

  private int ToChunk(int value) {
    return value/ this.model.Get<int>("chunkSize");;
  }
}
