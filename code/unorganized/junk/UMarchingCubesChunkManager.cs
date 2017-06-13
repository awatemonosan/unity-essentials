// using UnityEngine;

// using System;
// using System.Collections;
// using System.Collections.Generic;

// public class UMarchingCubesChunkManager : MonoBehaviour {
//   private const CHUNK_SIZE = 16;

//   void Start() {
//     this.GetDispatcher().On("model", delegate(object _){ this.Rebuild() });
//   }

//   public void Rebuild() {
//     //TODO: ONLY REBUILD CHANGED CHUNKS
//     this.gameObject.GetDispatcher().Trigger("rebuild");

//     Hashtable model = this.gameObject.GetModel();
//     UVoxelBlobController blobController = new UVoxelBlobController(model);
//     int xMin = model.Default<int>("xMin", 0);
//     int xMax = model.Default<int>("xMax", 0);
//     int yMin = model.Default<int>("yMin", 0);
//     int yMax = model.Default<int>("yMax", 0);
//     int zMin = model.Default<int>("zMin", 0);
//     int zMax = model.Default<int>("zMax", 0);

//     Hashtable voxelData = model.GetSub("voxelsData");
//     Hashtable chunksData = model.GetSub("chunksData");

//     for(int x = xMin; x <= xMax; x+=CHUNK_SIZE) {
//       int chunkX = x-(x%CHUNK_SIZE);
//       if(!chunksData.Has(chunkX)) { continue; }
//       for(int y = yMin; y <= yMax; y+=CHUNK_SIZE) {
//         int chunkY = y-(y%CHUNK_SIZE);
//         if(!chunksData.GetSub(chunkX).Has(chunkY)) { continue; }
//         for(int z = zMin; z <= zMax; z+=CHUNK_SIZE) {
//           int chunkZ = z-(z%CHUNK_SIZE);
//           if(!chunksData.GetSub(chunkX).GetSub(chunkY).Has(chunkZ)) { continue; }

//           GameObject chunk = new GameObject();
//           chunk.transform.parent = this.transform;
//           chunk.localPosition = Vector3.zero;
//           chunk.localRotation = Quaternion.zero;
//           chunk.localScale = Vector3.one;

//           UMarchingCubes marchingCubes = chunk.AddComponent<UMarchingCubesController>();
//           Vector3 windowStart = (new Vector3(chunkX,chunkY,chunkZ)) * CHUNK_SIZE;
//           Vector3 windowEnd = windowStart + (Vector3.one * CHUNK_SIZE);
//           marchingCubes.SetWindow(windowStart, windowEnd);

//           chunk.LoadModel(this.GetModel());

//           this.GetDispatcher.Once("rebuild", delegate(object _) {
//             GameObject.Destroy(chunk);
//           });
//         }
//       }
//     }
//   }
// }
