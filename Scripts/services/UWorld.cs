using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
 
public class UWorld : Singleton<UWorld>
{
  public int regionSize = 256;
  public int regionTerrainResolution = 256;

  public URegion GetRegionAt(Vector2)
  public UPromise Spawn(string entityName, UModel properties) {
    return new UPromise(delegate(Callback resolve, Callback reject) {
      bool isObserved = properties.Get<bool>("observed") || false;
      Vector2 point = properties.Get<Vector2>("point") || new Vector2(0,0);
      URegionController region = UWorld.GetRegionAt(point);

      if(!region.IsActive() && !isObserved) { resolve(new UModel()); }

      this.GetHeightAt(point)
        .Then(delegate(UModel heightData) {
          float y = heightData.Get<Float>("height");
          UModel response = new UModel();
          GameObject gameObject = GameObject.Instantiate(entityName, new Vector3(point.x, y, point.z));
          gameObject.GetModel().Merge(properties);
          response.Set("gameObject", gameObject);
          if (isObserved) { UWorld.Observe(gameObject); }
          resolve(response);
        });
    });
  }

  private UModel generatorCache = new UModel();
  public UPromise GetHeightAt(Vector2 point) {
    return new UPromise(delegate(Callback resolve, Callback reject) {
      UModel cachedModel = generatorCache.GetChild(point.x).GetChild(point.y);
      if (cachedModel.Has("height")) {
        resolve(cachedModel.Get<float>("height"));
      } else  {
        this.GenerateHeightAt(point)
          .Then(delegate(UModel heightData){
            float height = heightData.Get<float>("height");
            cachedModel.Set("height", height);
            resolve(height);
          });
      }
    });
  }

  private List<UPromise> heightGenerators = new List<UPromise>();
  private UPromise GenerateHeightAt(Vector2 point) {
    return UPromise.Reduce(this.heightGenerators, 0);
  }

  public void RegisterHeightGenerator(UPromise generator) {
    heightGenerators.Add(generator);

  }
}

public class URegionManager {
  public URegionController
}

public class URegionController {
  private UModel regionModel;
  private int regionSize;
  private Vector2 regionPosition;

  URegion(UModel regionModel, Vector2 regionPosition) {
    this.regionModel = regionModel;
    this.regionSize = regionModel.Get<int>("regionSize");
    this.regionPosition = regionModel.Get<Vector2>("regionPosition");
  }

  public void SetHeight(Vector2 point, float height) {
    point = point.Floor();
    regionModel.GetChild("maps").GetChild(point.x.ToString()).GetChild(point.y.ToString()).Set("height", height);
  }

  public float GetHeight(Vector2 point) {
    point = point.Floor();
    regionModel.GetChild("maps").GetChild(point.x.ToString()).GetChild(point.y.ToString()).Get<float>("height", height);
  }

  public UPromise RegenerateMaps(Callback generator) {
    List<UPromise> promises = new List<UPromise>();

    this.Reset();

    for (int x = regionPosition.x; x < regionPosition.x + regionSize; x ++) {
      for (int y = regionPosition.y; y < regionPosition.y + regionSize; y ++) {
        promises.Push( new UPromise( delegate(Callback resolve, Callback reject) {
          UModel generatorData = new UModel();
          generatorData.Set("x", x);
          generatorData.Set("y", y);

          generatorData.Set("height", 0);

          generator(generatorData)
            .then(delegate(UModel generatedData){
              float height = generatorData.Get<float>("height");
              this.SetHeight(new Vector2(x,y), height);
              resolve(generatedData);
            });
        }));
      }
    }

    return UPromise.All(promises);
  }
}