using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UVoxGenMultiplexer : UVoxelGenerator {
  private List<UVoxelGenerator> generators = new List<UVoxelGenerator>();

  public override void GenerateAt (Hashtable voxelData, int x, int y, int z) {
    for (int index = 0; index < generators.Count; index++) {
      this.generators[index].GenerateAt(voxelData, x, y, z);
    }
  }

  public void Add(UVoxelGenerator generator) {
    generators.Add(generator);
  }
}
