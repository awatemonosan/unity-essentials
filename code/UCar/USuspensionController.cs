using UnityEngine;

using System.Collections;

using Ukulele;

public class USuspensionController : MonoBehaviour
{
// config
    public float spring = 1000f;
    public float damper = 110f;
    public float targetPosition = 0.5f;

// Unity messages
    void Update ( )
    {
        this.UpdateWheelCollider();
    }

// Ukulele messages

// Private methods
    private void UpdateWheelCollider()
    {
        if(!this.HasComponent<WheelCollider>()) { return; }
        JointSpring suspension = this.GetComponent<WheelCollider>().suspensionSpring;
          suspension.spring         = this.spring;
          suspension.damper         = this.damper;
          suspension.targetPosition = this.targetPosition;
        this.GetComponent<WheelCollider>().suspensionSpring = suspension;
    }
}
