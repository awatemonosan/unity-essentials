// using UnityEngine;
// using System.Collections;
// [RequireComponent (typeof (WheelCollider))]
// public class WheelController : MonoBehaviour
// {
// 	//----------------------------------------
// 	// Properties
// 	//--------------------
// 	// Private
// 	private Vector3 modelOrigin=Vector3.zero;
// 	private float rotation=0;
// 	//--------------------
// 	// Public
// 	public float side=0;
// 	public bool hasMotor=false;
// 	public bool hasBrakes=false;
// 	public bool canSteer=false;

// 	public GameObject model;
// 	public WheelController crossWheel;
// 	//----------------------------------------
// 	// Advanced Properties
// 	//--------------------
// 	// Private
// 	public WheelCollider collider
// 	{
// 		get
// 		{
// 			return GetComponent<WheelCollider>();
// 		}
// 		set{}
// 	}
	
// 	public CarController carController;/*
// 	{
// 		get
// 		{
// 			CarController k=null;
// 			Transform t = transform;
// 			while (t.parent!=null)
// 			{
// 				if(k==null)
// 					Debug.Log("WheelController: " + t);
// 				if(t.GetComponent<CarController>())
// 					k=t.GetComponent<CarController>();
// 				t = t.parent;
// 			}
// 			if(k==null)
// 				Debug.Log("WheelController cannot find CarController");
// 			return k;
// 		}
// 		set{}
// 	}*/

// 	//--------------------
// 	// public
// 	private bool _isBroken=false;
// 	public bool isBroken
// 	{
// 		get
// 		{
// 			return _isBroken;
// 		}
// 		set{}
// 	}

// 	public float rpm
// 	{
// 		get
// 		{
// 			if (isBroken)
// 				return 0;
// 			return collider.rpm;
// 		}
// 		set{}
// 	}

// 	public float steerAngle
// 	{
// 		get
// 		{
// 			if (isBroken)
// 				return 0;
// 			else
// 				return collider.steerAngle;
// 		}
// 		set
// 		{
// 			if(isBroken)
// 				return;
// 			collider.steerAngle = value;
// 		}
// 	}
	
// 	public float brakeTorque
// 	{
// 		get
// 		{
// 			if(isBroken)
// 				return 0;
// 			else
// 				return collider.brakeTorque;
// 		}
// 		set
// 		{
// 			if(isBroken)
// 				return;
// 			collider.brakeTorque = value;
// 		}
// 	}
	
// 	public float motorTorque
// 	{
// 		get
// 		{
// 			if(isBroken)
// 				return 0;
// 			else
// 				return collider.motorTorque;
// 		}
// 		set
// 		{
// 			if(isBroken)
// 				return;
// 			collider.motorTorque = value;
// 		}
// 	}
// 	private WheelHit _hit;
// 	public WheelHit hit
// 	{
// 		get
// 		{
// 			if(isBroken)
// 				return _hit;
// 			collider.GetGroundHit(out _hit);
// 			return _hit;
// 		}
// 		set {}
// 	}

// 	public float suspensionFraction {
// 		get
// 		{
// 			if (collider.isGrounded)
// 				return Vector3.Dot(transform.position - hit.point, transform.up)/collider.suspensionDistance/2-collider.radius;
// 			return 1;
// 		}
// 		set {}
// 	}
// 	public Vector3 position {
// 		get
// 		{
// 			if(isBroken)
// 				return Vector3.zero;
// 			Vector3 lp=modelOrigin;
// 			if (collider.isGrounded) {
// 				lp.y -= (Vector3.Dot(transform.position - hit.point, transform.up) - collider.radius)/model.transform.parent.localScale.y;
// 			}
// 			else {
// 				lp.y -= (collider.suspensionDistance)/model.transform.parent.localScale.y;
// 			}
// 			return lp;
// 		}
// 		set {}
// 	}
// 	//----------------------------------------
// 	// Methods
// 	//--------------------
// 	// Private
	
// 	//--------------------
// 	// Public
// 	public GameObject Break()
// 	{
// 		if(isBroken)
// 			return null;
// 		//disconnect the wheel model and give it its own rigidbody
// 		model.transform.parent = null;
// 		model.AddComponent<CapsuleCollider> ();
// 		//Todo: Fix orientation of capsuule?
// 		model.AddComponent<Rigidbody> ();
// 		model.GetComponent<Rigidbody>().velocity=carController.GetComponent<Rigidbody>().velocity;
// 		model.GetComponent<Rigidbody>().angularVelocity=carController.GetComponent<Rigidbody>().angularVelocity;//+rpm;

// 		GameObject buf=model;
// 		model=null;

// 		//Destroy the wheel collider
// 		Component.Destroy(collider);
// 		//set broken to true
// 		_isBroken=true;
// 		//done breaking your toys
// 		return buf;
// 	}
	
// 	//----------------------------------------
// 	// Events
// 	void Start()
// 	{
// 		modelOrigin=model.transform.localPosition;
// 	}
// 	void FixedUpdate()
// 	{
// 		/*
// 		WheelFrictionCurve wfc = collider.forwardFriction;
// 		wfc.stiffness=1;
// 		collider.forwardFriction=wfc;
// 		wfc = collider.sidewaysFriction;
// 		wfc.stiffness=(Vector3.Dot (transform.forward,carController.rigidbody.velocity.normalized)+1)/2;
// 		//wfc.stiffness=1-Mathf.Abs(hit.sidewaysSlip/kart.rigidbody.velocity.magnitude);
// 		//wfc.asymptoteValue=300*(1-kart.rigidbody.velocity.magnitude/50);//1+kart.steerAngle*side*-1;
// 		collider.sidewaysFriction=wfc;
// 		*/
// 		if(isBroken)
// 			return;

// 		if (hasMotor) {
// 			// only set torque if wheel goes slower than the expected speed
// 			if (Mathf.Abs(rpm) > Mathf.Abs(carController.wantedRPM)) {
// 				// wheel goes too fast, set torque to 0
// 				motorTorque = 0;
// 			}
// 			else {
// 				// 
// 				float curTorque = motorTorque;
// 				motorTorque = curTorque * 0.9f + carController.newTorque * 0.1f;
// 			}
// 		}
// 		// check if we have to brake
// 		if (hasBrakes)
// 			brakeTorque = (carController.brake)?carController.brakeTorque:0.0f;
		
// 		// set steering angle
// 		if(canSteer)
// 			steerAngle = carController.steerAngle * carController.maxSteerAngle;
// 	}
// 	void Update ()
// 	{
// 		if(isBroken)
// 			return;
// 		//update the models location
// 		model.transform.localPosition=position;
// 		//update the models rotation
// 		rotation = Mathf.Repeat(rotation + Time.fixedDeltaTime * rpm * 360.0f / 60.0f, 360.0f);
// 		model.transform.localRotation = Quaternion.Euler(rotation, steerAngle, 0.0f);
// 		//Todo: apply skidding sounds
// 		//Todo: apply skidding effects
// 		//Todo: create ground particles (dust, mud, etc)
// 	}
// }
