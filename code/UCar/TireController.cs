using UnityEngine;

using System.Collections;

using Ukulele;

public class TireController : MonoBehaviour
{
    public float radius                 =   1.00f;
    public float mass                   =  10.00f;

    public float forwardAsymptoteSlip   =   2.00f;
    public float forwardAsymptoteValue  =   0.20f;
    public float forwardExtremumSlip    =   0.60f;
    public float forwardExtremumValue   =   1.20f;
    public float forwardStiffness       =   1.00f;

    public float sidewaysAsymptoteSlip  =   2.00f;
    public float sidewaysAsymptoteValue =   0.20f;
    public float sidewaysExtremumSlip   =   0.60f;
    public float sidewaysExtremumValue  =   1.20f;
    public float sidewaysStiffness      =   1.00f;

    private Quaternion offsetRotation;

// Unity Messages
    void Start ( )
    {
        this.offsetRotation = this.transform.rotation;
    }

    void Update ( )
    {
        this.UpdateView();
    }

    void FixedUpdate( )
    {
        this.UpdateWheelCollider();
    }

// Ukulele Messages

// Private Methods
    private void UpdateWheelCollider()
    {
        WheelFrictionCurve forwardFriction, sidewaysFriction;
        WheelCollider wheelCollider;

        if(!this.HasComponentInParent<WheelCollider>()) { return; }

        forwardFriction = new WheelFrictionCurve();
          forwardFriction.asymptoteSlip   = forwardAsymptoteSlip;
          forwardFriction.asymptoteValue  = forwardAsymptoteValue;
          forwardFriction.extremumSlip    = forwardExtremumSlip;
          forwardFriction.extremumValue   = forwardExtremumValue;
          forwardFriction.stiffness       = forwardStiffness;

        sidewaysFriction = new WheelFrictionCurve();
          sidewaysFriction.asymptoteSlip  = sidewaysAsymptoteSlip;
          sidewaysFriction.asymptoteValue = sidewaysAsymptoteValue;
          sidewaysFriction.extremumSlip   = sidewaysExtremumSlip;
          sidewaysFriction.extremumValue  = sidewaysExtremumValue;
          sidewaysFriction.stiffness      = sidewaysStiffness;

        wheelCollider = this.GetComponentInParent<WheelCollider>();
          wheelCollider.radius            = this.radius;
          wheelCollider.mass              = this.mass;
          wheelCollider.forwardFriction   = forwardFriction;
          wheelCollider.sidewaysFriction  = sidewaysFriction;
    }
    
    private void UpdateView()
    {
        if(!this.HasComponentInParent<WheelCollider>()) { return; }

        Vector3 wheelPosition;
        Quaternion wheelRotation;

        this.GetComponentInParent<WheelCollider>().GetWorldPose( out wheelPosition, out wheelRotation );

        this.transform.position = Vector3.Lerp(
          this.transform.position,
          wheelPosition,
          Time.deltaTime * 10
        );

        this.transform.rotation = Quaternion.Lerp(
          this.transform.rotation,
          wheelRotation * this.offsetRotation,
          Time.deltaTime * 10
        );
    }

// Public Methods

    // public void Break() {
    //     if(this.transform.parent == null) {return;}

    //     Vector3 wheelAngularVelocity = this.HasComponentInParent<WheelCollider>() ? new Vector3(wheelCollider.rpm, 0, 0) : Vector3.zero;
        
    //     Rigidbody rigidbody = this.Detach();
    //     rigidbody.weight = this.weight;
    //     rigidbody.angularVelocity = rigidbody.angularVelocity + wheelAngularVelocity;

    //     UCylinderCollider cylinderCollider = this.AddComponent<BoxCollider>();

    //     this.Remove();
    // }
}
