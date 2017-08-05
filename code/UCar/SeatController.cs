using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Ukulele;

public class SeatController : MonoBehaviour
{
    public int seatID;

// Custom Events
    void OnTriggerStayed(GameObject other)
    {
        ActorController actor = other.GetComponent<ActorController>();
        if(actor == null) // not an actor
        { return; }

        actor.Set("focusVehicle", this.GetComponentInParent<PhotonView>().viewID);
        actor.Set("focusSeat", this.seatID);
    }

    void OnTriggerExited(GameObject other)
    {
        ActorController actor = other.GetComponent<ActorController>();
        if(actor == null)
        { return; }
    }
// Public Methods
    public ActorController GetOccupant()
    {
        if(this.transform.childCount == 0)
        { return null; }

        ActorController actor = this.transform.GetChild(0).GetComponent<ActorController>();
        if(actor == null)
        { return null; }

        return actor;
    }
}
