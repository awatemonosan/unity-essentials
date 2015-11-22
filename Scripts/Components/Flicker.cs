using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviourExtended {
	public Vector3 area = new Vector3(0.1f,0.1f,0);
	public float speed = 0.1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.localPosition = Vector3.Lerp(transform.localPosition, area.Random (), speed);
	}
}
