using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UTerrainController : MonoBehaviour
{
  private UVoxelMapController voxelMapController;
  private UVoxelGenerator voxelGenerator;
  private UData chunkLoadedMap;

  public void Initialize (UVoxelMapController voxelMapController, UVoxelGenerator voxelGenerator) {
    this.voxelGenerator= voxelGenerator;
    this.voxelMapController= voxelMapController;
    this.chunkLoadedMap= new UData();

    this.gameObject.WithComponent<UMarchingCubesController>().Initialize(voxelMapController);
  }

  void Update () {
    this.LoadChunksAround(Camera.main.transform.position, 10);
  }

  public void LoadChunksAround (Vector3 point, float distance) {
    this.LoadChunksAround(Mathf.Floor(point.x), Mathf.Floor(point.y), Mathf.Floor(point.z), Mathf.Floor(distance));
  }

  public void LoadChunksAround(float x, float y, float z, float distance) {
    this.LoadChunksAround((int)Mathf.Floor(x), (int)Mathf.Floor(y), (int)Mathf.Floor(z), (int)Mathf.Floor(distance));
  }

  public void LoadChunksAround(int x, int y, int z, int distance) {
    Vector3 center = new Vector3(this.ToChunkSpace(x), this.ToChunkSpace(y), this.ToChunkSpace(z));
    List<Vector3> directions= new List<Vector3>{
      new Vector3( 0, 0, 1),
      new Vector3( 0, 0,-1),
      new Vector3( 1, 0, 0),
      new Vector3(-1, 0, 0),
      new Vector3( 0,-1, 0),
      new Vector3( 0, 1, 0)
    };


    UData checkedChunks= new UData();
    LinkedList<Vector3> checkQueue= new LinkedList<Vector3>();
    checkedChunks.GetChild(center.x).GetChild(center.y).Set(center.z, true);
    checkQueue.AddLast(center);

    float startTime = Time.realtimeSinceStartup;
    while (checkQueue.Count > 0) {
      Vector3 checkPos= checkQueue.First.Value;
      checkQueue.RemoveFirst();

      if (this.GenerateChunk(checkPos)) {
        float endTime = Time.realtimeSinceStartup;
        if (endTime - startTime > 0.05f) { return; }
      }

      for(int i= 0; i < 6; i++) {
        Vector3 offset = checkPos + directions[i];
        if(!checkedChunks.GetChild(offset.x).GetChild(offset.y).Has(offset.z) && UMath.IsVectorInSphere(offset, center, distance)) {
          checkedChunks.GetChild(offset.x).GetChild(offset.y).Set(offset.z, true);
          checkQueue.AddLast(offset);
        }
      }
    }
    // float endTime = Time.realtimeSinceStartup;
    // Debug.Log(endTime - startTime);
  }

  public bool GenerateChunk (Vector3 point) {
    point = point.Floor();
    return this.GenerateChunk((int)point.x, (int)point.y, (int)point.z);
  }

  public bool GenerateChunk (int x, int y, int z) {
    if(this.chunkLoadedMap.GetChild(x).GetChild(y).Has(z)) return false;
    this.chunkLoadedMap.GetChild(x).GetChild(y).Set(z, true);
    this.voxelGenerator.GenerateChunk(this.voxelMapController, x, y, z);
    return true;
  }

  private int ToChunkSpace (float v) {
    return (int)(Mathf.Floor(v/this.voxelMapController.GetChunkSize()));
  }
}
