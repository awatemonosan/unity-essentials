// using UnityEngine;
// using System.Collections;

// public class UEngine : MonoBehaviour {
// 	//Inputs
	
// 	public float _accellerator=0.0f;
// 	public float accellerator {
// 		get {
// 			return _accellerator;
// 		}
// 		set {
// 			_accellerator=Mathf.Clamp(value,-1,1);
// 		}
// 	}
// 	public float _steerAngle=0.0f;
// 	public float steerAngle {
// 		get {
// 			return _steerAngle;
// 		}
// 		set {
// 			_steerAngle=Mathf.Clamp(value,-1,1);
// 		}
// 	}
// 	public bool brake = false;
// 	public float shifter=0.0f;
	
// 	public Vector3 tilt=Vector3.zero;
	
// //settings
// 	public WheelController[] wheels;

// 	public float tiltForce=100;
// 	public float torque = 100f; // the base power of the engine (per wheel, and before gears)
// 	public float brakeTorque = 2000f; // the power of the braks (per wheel)
	
// 	public float maxSteerAngle = 30.0f; // max angle of steering wheels

// 	public float shiftDownRPM = 1500.0f; // rpm script will shift gear down
// 	public float shiftUpRPM = 2500.0f; // rpm script will shift gear up
// 	public float idleRPM = 500.0f; // idle rpm
// 	public float maxRPM = 5500.0f; // idle rpm
	
// 	// gear ratios (index 0 is reverse)
// 	public float[] gears = { -10f, 9f, 6f, 4.5f, 3f, 2.5f };
	
// 	// automatic, if true car shifts automatically up/down
// 	public bool automatic = true;
	
// 	public float killEngineSoundTimeout = 3.0f; // time until engine sound is cut off (in s.)
	
// 	public float faceForwardForce=0.1f;
// 	// table of efficiency at certain RPM, in tableStep RPM increases, 1.0f is 100% efficient
// 	// at the given RPM, current table has 100% at around 2000RPM
// 	float[] efficiencyTable = { 0.6f, 0.65f, 0.7f, 0.75f, 0.8f, 0.85f, 0.9f, 1.0f, 1.0f, 0.95f, 0.80f, 0.70f, 0.60f, 0.5f, 0.45f, 0.40f, 0.36f, 0.33f, 0.30f, 0.20f, 0.10f, 0.05f };
	
// 	// the scale of the indices in table, so with 250f, 750RPM translates to efficiencyTable[3].
// 	float efficiencyTableStep = 250.0f;
	
// 	Vector3 centerOfMass=Vector3.zero;
	
// 	int currentGear = 1; // duh.
	
// 	// shortcut to the component audiosource (engine sound).
// 	AudioSource audioSource;
	
	
// 	public float clutch=0;
// 	public float wantedClutch=0;
// 	public float newTorque = 0.0f;
// 	public float wantedRPM = 0.0f; // rpm the engine tries to reach
// 	public float motorRPM = 0.0f;
// 	public float killEngine = 0.0f;
	
// 	// Use this for initialization
// 	void Start () {
// 		centerOfMass=GetComponent<Rigidbody>().centerOfMass;
// 		// shortcut to audioSource should be engine sound, if null then no engine sound.
// 		audioSource = (AudioSource) GetComponent(typeof(AudioSource));
// 		if (audioSource == null) {
// 			Debug.Log("No audio source, add one to the car with looping engine noise (but can be turned off");
// 		}
// 	}
	
// 	void Update() {
// 		if (shifter>0) {
// 			ShiftUp();
// 		}
// 		if (shifter<1) {
// 			ShiftDown();
// 		}
// 	}
	
// 	float shiftDelay = 0.0f;
	
// 	// handle shifting a gear up
// 	public void ShiftUp() {
// 		float now = Time.timeSinceLevelLoad;
		
// 		// check if we have waited long enough to shift
// 		if (now < shiftDelay) return;
		
// 		// check if we can shift up
// 		if (currentGear < gears.Length - 1) {
// 			currentGear ++;
			
// 			// we delay the next shift with 1s. (sorry, hardcoded)
// 			shiftDelay = now + 1.0f;
// 		}
// 	}
	
// 	// handle shifting a gear down
// 	public void ShiftDown() {
// 		if(accellerator>0 && currentGear==1)
// 			return;
			
// 		float now = Time.timeSinceLevelLoad;
		
// 		// check if we have waited long enough to shift
// 		if (now < shiftDelay) return;
		
// 		// check if we can shift down (note gear 0 is reverse)
// 		if (currentGear > 0) {
// 			currentGear --;
			
// 			// we delay the next shift with 1/10s. (sorry, hardcoded)
// 			shiftDelay = now + 0.1f;
// 		}
// 	}

// 	// handle the physics of the engine
// 	void FixedUpdate () {
// 		GetComponent<Rigidbody>().AddRelativeTorque(tilt*tiltForce);
// 		float delta = Time.fixedDeltaTime;
		
// 		// handle automatic shifting
// 		if (automatic && (currentGear == 1) && (accellerator < 0.0f)) {
// 			ShiftDown(); // reverse
// 		}
// 		else if (automatic && (currentGear == 0) && (accellerator > 0.0f)) {
// 			ShiftUp(); // go from reverse to first gear
// 		}
// 		else if (automatic && (motorRPM > shiftUpRPM) && (accellerator > 0.0f)) {
// 			ShiftUp(); // shift up
// 		}
// 		else if (automatic && (motorRPM < shiftDownRPM) && (currentGear > 1)) {
// 			ShiftDown(); // shift down
// 		}
// 		if (automatic && (currentGear == 0)) {
// 			accellerator = - accellerator; // in automatic mode we need to hold arrow down for reverse
// 		}
// 		wantedClutch=1.0f;
// 		if (accellerator <= 0.0f) {
// 			// if we try to decelerate we brake.
// 			brake = true;
// 			accellerator = 0.0f;
// 			wantedRPM = 0.0f;
// 			wantedClutch=0;
// 		}
// 		clutch=Mathf.Lerp(clutch,wantedClutch,Time.deltaTime);
// 		// the RPM we try to achieve.
// 		wantedRPM = ((maxRPM * accellerator)*clutch+(idleRPM)*(1-clutch)) * 0.1f + wantedRPM * 0.9f;
		
// 		float rpm = 0.0f;
// 		int motorizedWheels = 0;
// 		bool floorContact = false;
		
// 		// calc rpm from current wheel speed and do some updating
// 		GetComponent<Rigidbody>().centerOfMass=centerOfMass;
// 		//faceForwardForce=(2-(Vector3.Dot (transform.forward,rigidbody.velocity.normalized)+1));
// 		foreach (WheelController w in wheels) {
// 			GetComponent<Rigidbody>().centerOfMass+=Vector3.right*w.side*w.suspensionFraction+Vector3.down*(1-w.suspensionFraction)/wheels.Length;
// 			// only calculate rpm on wheels that are connected to engine
// 			if (w.hasMotor) {
// 				rpm += w.collider.rpm;
// 				motorizedWheels++;
// 			}
// 		}
// 		// calculate the actual motor rpm from the wheels connected to the engine
// 		// note we haven't corrected for gear yet.
// 		if (motorizedWheels > 1) {
// 			rpm = rpm / motorizedWheels;
// 		}
		
// 		// we do some delay of the change (should take delta instead of just 95% of
// 		// previous rpm, and also adjust or gears.
// 		motorRPM = motorRPM*0.95f + 0.05f * Mathf.Abs(rpm * gears[currentGear]);
// 		if (motorRPM > maxRPM) motorRPM = maxRPM;
		
// 		// calculate the 'efficiency' (low or high rpm have lower efficiency then the
// 		// ideal efficiency, say 2000RPM, see table
// 		int index = (int) (motorRPM / efficiencyTableStep);
// 		if (index >= efficiencyTable.Length) index = efficiencyTable.Length - 1;
// 		if (index < 0) index = 0;
		
// 		// calculate torque using gears and efficiency table
// 		newTorque = torque * gears[currentGear] * efficiencyTable[index];
		
// 		// if we have an audiosource (motorsound) adjust pitch using rpm        
// 		if (audioSource != null) {
// 			// calculate pitch (keep it within reasonable bounds)
// 			float pitch = Mathf.Clamp(1.0f + ((motorRPM - idleRPM) / (shiftUpRPM - idleRPM) * 2.5f), 1.0f, 10.0f);
// 			audioSource.pitch = pitch;
			
// 			if (motorRPM > 100) {
// 				// turn on sound if it's not playing yet and RPM is > 100.
// 				if (!audioSource.isPlaying) {
// 					audioSource.Play();
// 				}
// 				// how long we should wait with engine RPM <= 100 before killing engine sound
// 				killEngine = Time.time + killEngineSoundTimeout;
// 			}
// 			else if ((audioSource.isPlaying) && (Time.time > killEngine)) {
// 				// standing still, kill engine sound.
// 				audioSource.Stop();
// 			}
// 		}
// 		//Keep facing forward
// 		if(!brake) {
// 		GetComponent<Rigidbody>().AddForceAtPosition(GetComponent<Rigidbody>().velocity.normalized*GetComponent<Rigidbody>().mass*faceForwardForce,GetComponent<Rigidbody>().worldCenterOfMass+transform.forward*2);
// 		GetComponent<Rigidbody>().AddForceAtPosition(GetComponent<Rigidbody>().velocity.normalized*GetComponent<Rigidbody>().mass*-faceForwardForce,GetComponent<Rigidbody>().worldCenterOfMass+transform.forward*-2);
// 		}
// 	}
// 	/*
// 	public void OnGUI() {
// 			// calculate actual speed in Km/H (SI metrics rule, so no inch, yard, foot,
// 			// stone, or other stupid length measure!)
// 			float speed = rigidbody.velocity.magnitude * 3.6f;
			
// 			// message to display
// 			string msg = "Speed " + speed.ToString("f0") + "Km/H, " + motorRPM.ToString("f0") + "RPM, gear " + currentGear; //  + " torque " + newTorque.ToString("f2") + ", efficiency " + table[index].ToString("f2");
			
// 			GUILayout.BeginArea(new Rect(Screen.width -250 - 32, 32, 250, 40), GUI.skin.window);
// 			GUILayout.Label(msg);
// 			GUILayout.EndArea();
// 	}
// 	*/
// }
