// using UnityEngine;

// using System;
// using System.Collections;
// using System.Collections.Generic;

// public class UVoxelTerrainManager : MonoBehaviour {
//   private const int CHUNK_SIZE = 16;

//   public delegate UData iUWorldGenerator(UData voxel, Vector3 point);
//   private List<iUWorldGenerator> generators;

//   private UData blobModel;
//   private UData terrainChunkControllers;
  
//   void Start() {
//     this.gameObject.GetDispatcher().On("model", delegate(UData _) { this.Ready(); });

//     this.Ready();
//   }

//   public void Ready() {
//     UData model = this.gameObject.GetModel();
//     this.chunkSize = model.Default<int>("chunkSize", 16);

//     this.xMin = model.Default<int>("xMin", 0);
//     this.xMax = model.Default<int>("xMax", 0);

//     this.yMin = model.Default<int>("yMin", 0);
//     this.yMax = model.Default<int>("yMax", 0);

//     this.zMin = model.Default<int>("zMin", 0);
//     this.zMax = model.Default<int>("zMax", 0);

//     this.blobModel = model.GetChild("blobModel");
//     this.terrainChunkControllers = model.GetChild("_terrainChunkControllers");

//     this.gameObject.GetDispatcher().Trigger("ready");
//   }

//   public void AddGenerator(iUWorldGenerator generator) {
//     this.generators.Add(generator);
//     this.Ready();
//   }

//   public void Touch(Vector3 point, float timeout) {
//     this.WithTerrainChunkAt(point).destroyer.SetTimeout(timeout);
//   }

//   private GameObject WithTerrainChunkAt(Vector3 point) {
//     Vector3 chunkPoint = this.WorldPointToChunkPoint(point);
//     if(!this.HasTerrainChunkAt(point)) { return this.CreateTerrainChunkAt(point); }
//     return this.terrainChunkControllers.GetChild(point.x).GetChild(point.y).Get<UVoxelTerrainChunkController>(point.z);
//   }

//   public UVoxelController GetVoxelAt(Vector3 point) {
//     if(!this.HasVoxelAt(point)) { return null; }
//     return this.WithVoxelAt(point);
//   }
  
//   public UVoxelController WithVoxelAt(Vector3 point) {
//     if(!this.HasVoxelAt(point)) { this.GenerateVoxelAt(point); }
//     return this.WithChunkAt(point).GetVoxelAt(point);
//   }

//   public void GenerateVoxelAt(Vector3 point) {
//     Vector3 voxelPoint = point.Floor();
//     UData generatedVoxelModel = generators.Reduce<iUWorldGenerator, UData>(new UData(), delegate(UData voxelModel, int index, iUWorldGenerator generator, List<iUWorldGenerator> list){
//       return generator(voxelModel, voxelPoint);
//     });
//     this.SetVoxelAt(point, generatedVoxelModel);
//   }

//   public bool HasVoxelAt(Vector3 point) {
//     if(!this.HasChunkAt(point)) { return false; }
//     if(!this.GetChunkAt(point).HasVoxelAt(point)) { return false; }
//     return true;
//   }

//   public void RemoveVoxelAt(Vector3 point) {
//     this.WithChunkAt(point).RemoveVoxelAt(point);
//   }

//   public void SetVoxelAt(Vector3 point, UData voxelModel) {
//     this.WithChunkAt(point).SetVoxelAt(point, voxelModel);
//   }

//   public UChunkController GetChunkAt(Vector3 point) {
//     if(!this.HasChunkAt(point)) { return null; }
//     return this.WithChunkAt(point);
//   }

//   public bool HasChunkAt(Vector3 point) {
//     Vector3 chunkPoint = this.WorldPointToChunkPoint(point);
//     return this.gameObject.GetModel().GetChild("chunks").GetChild(chunkPoint.x).GetChild(chunkPoint.y).Has(chunkPoint.z);
//   }

//   public UChunkController WithChunkAt(Vector3 point) {
//     Vector3 chunkPoint = this.WorldPointToChunkPoint(point);
//     UData chunkModel = this.gameObject.GetModel().GetChild("chunks").GetChild(chunkPoint.x).GetChild(chunkPoint.y).GetChild(chunkPoint.z);
//     return new UChunkController(chunkModel, point);
//   }

//   private Vector3 WorldPointToChunkPoint(Vector3 point) {
//     return (point / CHUNK_SIZE).Floor();
//   }
// }

// public class UChunkController {
//   private UData chunkModel;
//   private UData voxelsModel;

//   public UChunkController(UData chunkMode, Vector3 origin) {
//     this.chunkModel = chunkModel;
//     this.voxelsModel = chunkModel.GetChild("voxels");

//     this.chunkModel.Default("origin", origin);
//   }

//   public UVoxelController GetVoxelAt(Vector3 point) {
//     if(!this.HasVoxelAt(point)) { return null; }
//     Vector3 voxelPoint = point.Floor();
//     return this.WithVoxelAt(point);
//   }

//   public UVoxelController WithVoxelAt(Vector3 point) {
//     Vector3 voxelPoint = point.Floor();
//     return new UVoxelController(chunkModel.GetChild(voxelPoint.x).GetChild(voxelPoint.y).GetChild(voxelPoint.z));
//   }

//   public bool HasVoxelAt(Vector3 point) {
//     Vector3 voxelPoint = point.Floor();
//     return voxelsModel.GetChild(voxelPoint.x).GetChild(voxelPoint.y).Has(voxelPoint.z);
//   }

//   public void RemoveVoxelAt(Vector3 point) {
//     Vector3 voxelPoint = point.Floor();
//     voxelsModel.GetChild(voxelPoint.x).GetChild(voxelPoint.y).Remove(voxelPoint.z);
//   }

//   public void SetVoxelAt(Vector3 point, UData voxelModel) {
//     Vector3 voxelPoint = point.Floor();
//     voxelsModel.GetChild(voxelPoint.x).GetChild(voxelPoint.y).GetChild(voxelPoint.z).Merge(voxelModel);
//   }
// }

// public class UVoxelController {
//   private UData voxelModel;
//   public UVoxelController(UData voxelModel) {
//     this.voxelModel = voxelModel;
//   }
// }
// ///////////////////////////////////////////////////////////////////////////////
// // using UnityEngine;

// // using System;
// // using System.Collections;
// // using System.Collections.Generic;
 
// // public class UWorld : Singleton<UWorld>
// // {
// //   public float GetHeightAt(Vector2 point) {
// //     if(!this.model.GetChild("height").HasChild(point.x)) { return this.GenerateHeightAt( point ); }
// //     if(!this.model.GetChild("height").GetChild(point.x).HasChild(point.y)) { return this.GenerateHeightAt( point ); }
// //     return this.model.GetChild("height").GetChild(point.x).Get<float>(point.y);
// //   }

// //   public float GenerateHeightAt(Vector2 point) {
// //     int zoneID = this.GetZoneAt(point);
// //     if (zoneID == -1) {
// //       return 0;
// //     }
// //     float height =  this.GetHeightInZoneAt(zoneID, point);
// //     this.model.GetChild("height").GetChild(point.x).Set(point.y, height);
// //     return height;
// //   }

// //   public int GetZoneAt (Vector2 point) {
// //     int zoneID = -1;
// //     this.model.GetChild("zones").Each(delegate(UData kvp) {
// //       string key = kvp.Get("key");
// //       UData child = kvp.Get<UData>("value");
// //       List<Vector2> points = new List<Vector2>();
// //       child.Each(delegate(UData childKVP) {
// //         Vector2 point = kvp.Get<Vector2>("value");
// //         points.Add(point);  
// //       });
// //       if (this.isIn(point, points)) { zoneID  = key; }
// //     });
// //     return zoneID;
// //   } 

// //   public int CreateZone (List<int> points)  {
// //     this.model.GetChild("zones").Push(new UData(points));
// //     return this.model.GetChild("zones").Length() - 1; 
// //   }

// //   public int AddPoint (Vector2 point) {
// //     this.model.GetChild("points").Push(new UData(point));
// //     return this.model.GetChild("points").Length() - 1;
// //   }
// // }


// //   //old

// //   public UPromise Spawn(string entityName, UData properties) {
// //     return new UPromise(delegate(Callback resolve, Callback reject) {
// //       bool isObserved = properties.Get<bool>("observed") || false;
// //       Vector2 point = properties.Get<Vector2>("point") || new Vector2(0,0);
// //       UChunkController chunk = UWorld.GetChunkAt(point);

// //       if(!chunk.IsActive() && !isObserved) { resolve(new UData()); }

// //       this.GetHeightAt(point)
// //         .Then(delegate(UData heightData) {
// //           float y = heightData.Get<Float>("height");
// //           UData response = new UData();
// //           GameObject gameObject = GameObject.Instantiate(entityName, new Vector3(point.x, y, point.z));
// //           gameObject.GetModel().Merge(properties);
// //           response.Set("gameObject", gameObject);
// //           if (isObserved) { UWorld.Observe(gameObject); }
// //           resolve(response);
// //         });
// //     });
// //   }

// //   private UData generatorCache = new UData();
// //   public UPromise GetHeightAt(Vector2 point) {
// //     return new UPromise(delegate(Callback resolve, Callback reject) {
// //       UData cachedModel = generatorCache.GetChild(point.x).GetChild(point.y);
// //       if (cachedModel.Has("height")) {
// //         resolve(cachedModel.Get<float>("height"));
// //       } else  {
// //         this.GenerateHeightAt(point)
// //           .Then(delegate(UData heightData){
// //             float height = heightData.Get<float>("height");
// //             cachedModel.Set("height", height);
// //             resolve(height);
// //           });
// //       }
// //     });
// //   }

// //   private List<UPromise> heightGenerators = new List<UPromise>();
// //   private UPromise GenerateHeightAt(Vector2 point) {
// //     return UPromise.Reduce(this.heightGenerators, 0);
// //   }

// //   public void RegisterHeightGenerator(UPromise generator) {
// //     heightGenerators.Add(generator);

// //   }
// // }

// // public class UChunkManager {
// //   public UChunkController
// // }

// // public class UChunkController {
// //   private UData chunkModel;
// //   private int chunkSize;
// //   private Vector2 chunkPosition;

// //   UChunk(UData chunkModel, Vector2 chunkPosition) {
// //     this.chunkModel = chunkModel;
// //     this.chunkSize = chunkModel.Get<int>("chunkSize");
// //     this.chunkPosition = chunkModel.Get<Vector2>("chunkPosition");
// //   }

// //   public void SetHeight(Vector2 point, float height) {
// //     point = point.Floor();
// //     chunkModel.GetChild("maps").GetChild(point.x.ToString()).GetChild(point.y.ToString()).Set("height", height);
// //   }

// //   public float GetHeight(Vector2 point) {
// //     point = point.Floor();
// //     chunkModel.GetChild("maps").GetChild(point.x.ToString()).GetChild(point.y.ToString()).Get<float>("height", height);
// //   }

// //   public UPromise RegenerateMaps(Callback generator) {
// //     List<UPromise> promises = new List<UPromise>();

// //     this.Reset();

// //     for (int x = chunkPosition.x; x < chunkPosition.x + chunkSize; x ++) {
// //       for (int y = chunkPosition.y; y < chunkPosition.y + chunkSize; y ++) {
// //         promises.Push( new UPromise( delegate(Callback resolve, Callback reject) {
// //           UData generatorData = new UData();
// //           generatorData.Set("x", x);
// //           generatorData.Set("y", y);

// //           generatorData.Set("height", 0);

// //           generator(generatorData)
// //             .then(delegate(UData generatedData){
// //               float height = generatorData.Get<float>("height");
// //               this.SetHeight(new Vector2(x,y), height);
// //               resolve(generatedData);
// //             });
// //         }));
// //       }
// //     }

// //     return UPromise.All(promises);
// //   }
// // }