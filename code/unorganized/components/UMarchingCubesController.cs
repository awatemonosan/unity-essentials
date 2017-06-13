using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using Ukulele;

// [RequireComponent(typeof(MeshFilter))]
// [RequireComponent(typeof(MeshRenderer))]
// [RequireComponent(typeof(MeshCollider))]
public class UMarchingCubesController : MonoBehaviour {
  Material material;

  UVoxelMapController voxelMapController;
  // UEmiterInterface voxelMapEmiter;
  Hashtable chunkMap = new Hashtable();

  public void Initialize(UVoxelMapController voxelMapController) {
  // public void Initialize(UVoxelMapController voxelMapController, Material material) {
    // this.material = material;

    this.voxelMapController = voxelMapController;
    this.voxelMapController.dispatcher.On("chunk_created", delegate(Hashtable chunkInfo) {
      int x = chunkInfo.Get<int>("x");
      int y = chunkInfo.Get<int>("y");
      int z = chunkInfo.Get<int>("z");

      this.CreateChunk(x, y, z);
    });

    this.voxelMapController.dispatcher.On("chunk_updated", delegate(Hashtable chunkInfo) {
      int x = chunkInfo.Get<int>("x");
      int y = chunkInfo.Get<int>("y");
      int z = chunkInfo.Get<int>("z");
      
      this.UpdateChunk(x, y, z);
    });

    this.voxelMapController.dispatcher.On("chunk_destroyed", delegate(Hashtable chunkInfo) {
      int x = chunkInfo.Get<int>("x");
      int y = chunkInfo.Get<int>("y");
      int z = chunkInfo.Get<int>("z");

      this.DestroyChunk(x, y, z);
    });
  }

  private void CreateChunk(int x, int y, int z) {
    GameObject chunk = new GameObject();
    chunk.transform.parent = this.transform;
    chunk.transform.Align(this.transform);
    this.chunkMap.GetSub(x).GetSub(y).Set(z, chunk);

    this.UpdateChunk(x, y, z);
  }

  private void UpdateChunk(int x, int y, int z) {
    int chunkSize = this.voxelMapController.GetChunkSize();
    GameObject chunk = this.chunkMap.GetSub(x).GetSub(y).Get<GameObject>(z);

    int xStart = x * chunkSize;
    int yStart = y * chunkSize;
    int zStart = z * chunkSize;
    int xEnd = (x + 1) * chunkSize;
    int yEnd = (y + 1) * chunkSize;
    int zEnd = (z + 1) * chunkSize;

    Mesh mesh = UMarchingCubes.GenerateMesh(this.voxelMapController, xStart, yStart, zStart, xEnd, yEnd, zEnd, true);

    MeshFilter meshFilter = chunk.WithComponent<MeshFilter>();
    MeshCollider meshCollider = chunk.WithComponent<MeshCollider>();
    MeshRenderer meshRenderer = chunk.WithComponent<MeshRenderer>();

    meshRenderer.material = this.material;
    meshFilter.mesh = mesh;
    meshCollider.sharedMesh = mesh;
    // chunk.WithComponent<MeshCollider>().sharedMesh = mesh;
  } 

  private void DestroyChunk(int x, int y, int z) {
    if(chunkMap.GetSub(x).GetSub(y).Has(z)) {
      GameObject.Destroy(chunkMap.GetSub(x).GetSub(y).Get<GameObject>(z));
      chunkMap.GetSub(x).GetSub(y).Remove(z);
    }
  }
}
