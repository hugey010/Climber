using UnityEngine;
using System.Collections;

public class StickyHand : MonoBehaviour
{

	public Rigidbody rootRigidBody;

	private	 bool StickOn = true;
	private Vector3 stickPosition = Vector3.zero;

	public void setStickOn (bool isOn)
	{
		StickOn = isOn;
		if (!isOn) {
			stickPosition = Vector3.zero;
		} 
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			setStickOn (!StickOn);
			//toggleGravity (rootRigidBody, true);
		}

		if (StickOn && stickPosition != Vector3.zero) {
			transform.position = stickPosition;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		Debug.Log ("trigger enter");
		if (StickOn && other.tag == "Environment" && stickPosition == Vector3.zero) {
			stickPosition = transform.position;
			rootRigidBody.velocity = Vector3.zero;
			//toggleGravity (rootRigidBody, false);
		}
	}

	private void toggleGravity (Rigidbody rigidBody, bool enabled)
	{
		Rigidbody[] rbs = rigidBody.transform.GetComponentsInChildren<Rigidbody> ();
		foreach (Rigidbody r in rbs) {
			r.mass = enabled ? 0.5f : 0.00001f;
		}
	}
}
