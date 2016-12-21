using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Impact 
{
  public Vector3 normal;
  public Collider otherCollider;
  public Vector3 point;
  public Collider thisCollider;
  
  public GameObject thisGameObject;
  public GameObject otherGameObject;
  public Vector3 relativeVelocity;
}

public class RigidbodyExtensionController : MonoBehaviour
{
  public float maxSlope = 45;
  public Vector3 groundNormal = Physics.gravity;
  private Hashtable impacts = new Hashtable();
  void Start()
  {
    gameObject.GetDispatcher().On("late_update", this.ResetEverything);
    gameObject.GetDispatcher().On("collision_stay", this.HandleCollision);
  }

  public bool IsOnGround()
  {
    return Vector3.Dot(groundNormal, Physics.gravity.normalized) > ((1f-maxSlope/180)*2f-1f);
  }

  public bool IsTouching(Object other)
  {
    return impacts.ContainsKey(other);
  }

  private bool HandleCollision(Hashtable payload)
  {
    Collision collision = (Collision)payload.GetAs<Collision>("collision");
    foreach( ContactPoint contact in collision.contacts )
    {
      //Register impact
      Impact impact = new Impact();

      impact.thisGameObject   = gameObject;
      impact.otherGameObject  = collision.gameObject;
      impact.relativeVelocity = collision.relativeVelocity;
      impact.normal           = contact.normal;
      impact.point            = contact.point;
      impact.thisCollider     = contact.thisCollider;
      impact.otherCollider    = contact.otherCollider;

      impacts.Add(impact.otherGameObject, impact);
      impacts.Add(impact.otherCollider, impact);
      impacts.Add(collision.rigidbody, impact);

      //Update OnGround
      float oldDot = Vector3.Dot(groundNormal, Physics.gravity.normalized);
      float newDot = Vector3.Dot(contact.normal, Physics.gravity.normalized);
      if(newDot > oldDot)
        groundNormal = contact.normal;
    }
    return true;
  }

  public bool ResetEverything(Hashtable _)
  {
    impacts = new Hashtable();
    groundNormal = Physics.gravity * -1;
    return true;
  }

}
