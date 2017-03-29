using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UObjectClickActivatorEngine : Singleton<UObjectClickActivatorEngine>
{
  private GameObject hovered;
  private UModel hitModelBuffer = new UModel();

  void Start()
  {
    UInput.GetDispatcher().On("mouse.button", HandleGameObjectClicked);
  }

  private bool HandleGameObjectClicked(UModel payload)
  {
    UModel combinedPayload = new UModel(hitModelBuffer).Merge(payload);
    if (this.hovered != null) this.hovered.GetDispatcher().Trigger(combinedPayload);
    return true;
  }

  void Update()
  {
    UpdateHoveredGameObject();
  }

  private void UpdateHoveredGameObject()
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hitInfo = new RaycastHit();
    GameObject subject = null;

    bool hit = Physics.Raycast(ray, out hitInfo);

    this.hitModelBuffer = new UModel();
    this.hitModelBuffer.Set("hit", hit);
    if (hit)
    {
      hitModelBuffer.Set("oldGameObject", this.hovered);
      hitModelBuffer.Set("gameObject", hitInfo.transform.gameObject);

      hitModelBuffer.Set("barycentricCoordinate", hitInfo.barycentricCoordinate);
      hitModelBuffer.Set("collider", hitInfo.collider);
      hitModelBuffer.Set("distance", hitInfo.distance);
      hitModelBuffer.Set("lightmapCoord", hitInfo.lightmapCoord);
      hitModelBuffer.Set("normal", hitInfo.normal);
      hitModelBuffer.Set("point", hitInfo.point);
      hitModelBuffer.Set("rigidbody", hitInfo.rigidbody);
      hitModelBuffer.Set("textureCoord", hitInfo.textureCoord);
      hitModelBuffer.Set("textureCoord2", hitInfo.textureCoord2);
      hitModelBuffer.Set("transform", hitInfo.transform);
      hitModelBuffer.Set("triangleIndex", hitInfo.triangleIndex);
      
      subject = hitInfo.transform.gameObject;
    }

    if (this.hovered != subject)
    {
      if (this.hovered != null)
      {
        this.hovered.GetDispatcher().Trigger("unhover", hitModelBuffer);
      }
      if (subject != null)
      {
        subject.GetDispatcher().Trigger("hover", hitModelBuffer);
      }
      this.hovered = subject;
    }
  }
}
