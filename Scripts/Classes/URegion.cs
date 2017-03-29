using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
 
public class URegion {
  private UWorldGenerator worldGenerator;
  public URegion(UWorldGenerator worldGenerator) {
    this.worldGenerator = worldGenerator;
  }

  UModel cache = new UModel();
  public UPromise GetHeightAt(Vector2 point) {
    return new UPromise(delegate(Callback resolve, Callback reject) {
      string key = point.ToString();
      if (cache.Has(key)) {
        UModel heightData = new UModel();
        float height = cache.Get<float>(key);
        heightData.Set("height", height)
        resolve(heightData);
      } else {
        this.worldGenerator.GenerateHeightAt(point)
          .Then(delegate(UModel heightData) {
            float height = heightData.Get<float>("height");
            cache.Set(key, height);
            resolve(heightData);
          });
      }
    });
  }

  private UPromise GenerateHeightAt(Vector2 point) {
    return new UPromise(delegate(Callback resolve, Callback reject) {
      resolve(0);
    });
  }
}
