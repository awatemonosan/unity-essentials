using UnityEngine;

using System.Collections;

using Ukulele;

namespace Ukulele
{
    public class WheelController : MonoBehaviour
    {
    // config
        public float motorFactor = 0.0f;
        public float steerFactor = 0.0f;

        public float maxBrakeTorque = 100f;

    // outputs
        public float currentTorque;
        public float currentBrakeTorque;
        public float currentSteerAngle;
        public float currentRPM;

    // Unity messages
        void Update ( )
        {
            this.UpdateOutputs( );
        }

        void FixedUpdate ( )
        {
            this.UpdateWheelCollider();
        }

    // Ukulele messages

    // Private methods
        private void UpdateOutputs( )
        {
            // if(!this.HasComponentInChildren<TireController>()) { return; }
            if(!this.AssertHasComponentInParent<CarController>()) { return; }

            CarController carController = this.GetComponentInParent<CarController>();

            if(this.HasComponent<WheelCollider>())
            {
                this.currentRPM     = this.GetComponent<WheelCollider>().rpm;
                this.currentTorque  = ( this.currentRPM < carController.targetWheelRPM )
                                    ? carController.targetWheelTorque * this.motorFactor
                                    : 0;
            }
            else
            {
                this.currentRPM     = carController.targetWheelRPM;
                this.currentTorque  = carController.targetWheelTorque * this.motorFactor;
            }

            this.currentBrakeTorque = carController.targetWheelBrakeFactor * this.maxBrakeTorque;
            this.currentSteerAngle  = carController.targetWheelSteer       * this.steerFactor;
        }

        private void UpdateWheelCollider()
        {
            if(this.HasComponentInChildren<TireController>())
            {
                WheelCollider wheelCollider = this.WithComponent<WheelCollider>();
                  wheelCollider.motorTorque = this.currentTorque;
                  wheelCollider.brakeTorque = this.currentBrakeTorque;
                  wheelCollider.steerAngle  = this.currentSteerAngle;
            }
            else if(this.HasComponent<WheelCollider>())
            {
                this.RemoveComponent<WheelCollider>();
            }
        }
    }
}
