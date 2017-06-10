using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UCarController : MonoBehaviour
{
// references
    private InputLayer inputLayer;

// Config
    public float idleEngineRPM       = 500.0f; // idle rpm
    public float maxEngineRPM        = 5500.0f; // idle rpm

    public float baseTorque          = 100f; // the base power of the engine (per wheel, and before gears)
    public float maxSteerAngle       = 30.0f; // max angle of steering wheels

    public float[] gears             = { -10f, 9f, 6f, 4.5f, 3f, 2.5f };
    private float[] efficiencyTable          = { 0.6f, 0.65f, 0.7f, 0.75f, 0.8f, 0.85f, 0.9f, 1.0f, 1.0f, 0.95f, 0.80f, 0.70f, 0.60f, 0.5f, 0.45f, 0.40f, 0.36f, 0.33f, 0.30f, 0.20f, 0.10f, 0.05f };
    private float efficiencyTableStep        = 250.0f;

// inputs
    public float accell = 0;
    public float clutch = 0;
    public float brake = 0;
    public float steer = 0;

    public bool handBrake = true;
    public bool lights = false;
    public bool engine = true;

// outputs
    public float currentEngineRPM;
    public float currentMotorizedWheelsRPM;
    public float currentEngineTorque;

    public float targetWheelRPM;
    public float targetWheelTorque;
    public float targetWheelBrakeFactor;
    public float targetWheelSteer;

// dynamic
    int currentGear = 1;

// Unity messages
    void Start ( )
    {
        this.inputLayer = this.gameObject.WithComponent<InputLayer>();
        this.inputLayer.master = this.transform.parent.WithComponent<InputLayer>();
    }

    void Update() {
        this.UpdateInputs();
    }

    void FixedUpdate ( )
    {
        this.UpdateOutputs();
    }

// Private methods
    private void UpdateInputs()
    {
        if(this.inputLayer.master) {
            if(this.currentGear <= 1)
            {
                this.currentGear = this.inputLayer.moveDirection.z < 0 ? 0 : 1;
                this.accell = Mathf.Abs(this.inputLayer.moveDirection.z);
                this.clutch = 1 - this.accell;
            }
            else
            {
                this.accell = Mathf.Max(0, this.inputLayer.moveDirection.z);
                this.brake = Mathf.Max(0, this.inputLayer.moveDirection.z * -1);
                this.clutch = this.brake + 1 - this.accell;
            }

            this.steer = this.inputLayer.moveDirection.x;
            this.handBrake = this.inputLayer.moveDirection.y > 0.5f;
        } else {
            this.accell = 0;
            this.clutch = 0;
            this.brake = 0;
        }
    }

    private void UpdateOutputs()
    {
        this.currentEngineRPM          = this.GetCurrentEngineRPM();
        this.currentMotorizedWheelsRPM = this.GetCurrentMotorizedWheelsRPM();
        this.currentEngineTorque       = this.GetCurrentEngineTorque();

        this.targetWheelRPM            = this.GetTargetWheelRPM();
        this.targetWheelTorque         = this.GetWheelTorque();
        this.targetWheelBrakeFactor         = this.GettargetWheelBrakeFactor();
        this.targetWheelSteer               = this.GetTargetWheelSteer();
    }

    private float GetCurrentEngineRPM() {
        return UMath.Clamp(
          Mathf.Abs(this.currentMotorizedWheelsRPM * gears[currentGear]),
          0,
          maxEngineRPM
        );
    }

    private float GetCurrentMotorizedWheelsRPM ()
    {
        float totalWheelRPM = 0;
        float motorizedWheelsCount = 0;

        foreach (UWheelController wheelController in this.GetComponentsInChildren<UWheelController>()) {
            totalWheelRPM += Mathf.Abs(wheelController.currentRPM * wheelController.motorFactor);
            motorizedWheelsCount += wheelController.motorFactor;
        }
        
        return motorizedWheelsCount > 0 ? totalWheelRPM /= motorizedWheelsCount : 0;
    }

    // private void CleanOutputs ( )
    // {
    //     this.targetWheelRPM                 = Mathf.Max( this.targetWheelRPM, 0 );
    //     this.targetWheelTorque = Mathf.Max( this.targetWheelTorque, 0 );
    //     this.targetWheelBrakeFactor = Mathf.Max( this.targetWheelBrakeFactor, 0 );
    //     this.targetWheelSteer       = UMath.Clamp( this.targetWheelSteer, -180,  180 );
    // }

    private float GetCurrentEngineTorque()
    {
        int efficiencyTableIndex = UMath.Clamp(
          (int)(this.currentMotorizedWheelsRPM / this.efficiencyTableStep),
          0,
          this.efficiencyTable.Length - 1
        );
        return this.baseTorque * this.gears[currentGear] * this.efficiencyTable[efficiencyTableIndex];
    }

    private float GetTargetWheelRPM()
    {
        if (!this.engine) { return 0; }

        float idleMaxDifference = this.maxEngineRPM - this.idleEngineRPM;
        float desiredRPM = idleMaxDifference * this.accell; 
        float targetWheelRPM = this.idleEngineRPM + desiredRPM;

        return targetWheelRPM;
    }

    private float GetWheelTorque() {
        return this.currentEngineTorque * (1 - this.clutch) * this.accell;
    }

    private float GettargetWheelBrakeFactor()
    {
        return ( !this.handBrake ) ? this.brake : 1;
    }

    private float GetTargetWheelSteer()
    {
        return this.steer * 180;
    }
}
