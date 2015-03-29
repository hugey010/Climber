using UnityEngine;
using System.Collections;

public class StickyHand : MonoBehaviour
{

	public bool StickOn;

	private Vector3 stickPosition = Vector3.zero;

	// Use this for initialization
	void Start ()
	{
		StickOn = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (stickPosition != Vector3.zero) {
			transform.position = stickPosition;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Environment") {
			stickPosition = transform.position;
		}

	}
}
