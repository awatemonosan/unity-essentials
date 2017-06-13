using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using Ukulele;

public class UVoxelGenerator {
  public virtual void GenerateAt (Hashtable voxelData, int x, int y, int z) {
    // Debug.Log("GenerateAt" + " " + x + " " + y + " " + z);
  }

  public void GenerateChunk(UVoxelMapController voxelMapController, int xChunk, int yChunk, int zChunk) {
    int x, y, z;
    int chunkSize = voxelMapController.GetChunkSize();

    for (x = xChunk * chunkSize; x < (xChunk+1) * chunkSize; x++) {
      for (y = yChunk * chunkSize; y < (yChunk+1) * chunkSize; y++) {
        for (z = zChunk * chunkSize; z < (zChunk+1) * chunkSize; z++) {
          this.GenerateAt(voxelMapController.WithAt(x, y, z), x, y, z);
        }
      }
    }

    Hashtable chunkData = new Hashtable();
    chunkData.Set("x", xChunk);
    chunkData.Set("y", yChunk);
    chunkData.Set("z", zChunk);

    voxelMapController.dispatcher.Trigger("chunk_updated", chunkData);
  }
}
