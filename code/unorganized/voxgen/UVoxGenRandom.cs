using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UVoxGenRandom : UVoxelGenerator {
  public override void GenerateAt (Hashtable voxelData, int x, int y, int z) {
    voxelData.Set("fill", Random.value);
  }
}
