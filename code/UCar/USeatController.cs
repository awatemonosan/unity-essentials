using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class USeatController : MonoBehaviour
{
    public int seatID;

// Custom Events
    void OnTriggerStayed(GameObject other)
    {
        ActorController actor = other.GetComponent<ActorController>();
        if(actor == null) // not an actor
        { return; }

        actor.vehiclePhotonID = this.GetComponentInParent<PhotonView>().viewID;
        actor.vehicleSeat = this.seatID;
        actor.vehicleCanEnter = true;
    }

    void OnTriggerExited(GameObject other)
    {
        ActorController actor = other.GetComponent<ActorController>();
        if(actor == null)
        { return; }

        actor.vehicleCanEnter = false;
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
