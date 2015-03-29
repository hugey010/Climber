using UnityEngine;
using System.Collections;

public class CharacterInput : MonoBehaviour
{

	public float BASE_FORCE = 30f;
	public Rigidbody rootRigidBody;

	private Vector3 startDragPoint;
	private Vector3 endDragPoint;
	private float zPosition;

	void Start ()
	{
		zPosition = rootRigidBody.position.z;
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			startDragPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, zPosition);
			
		}
		if (Input.GetMouseButtonUp (0)) {
			endDragPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, zPosition);
			jump ();
		}

		orient ();
	}

	private void jump ()
	{
		Vector3 dragLine = startDragPoint - endDragPoint;
		float force = dragLine.magnitude * BASE_FORCE;
		Vector3 direction = dragLine.normalized;
		rootRigidBody.AddForce (direction * force);

		Rigidbody[] children = rootRigidBody.GetComponentsInChildren<Rigidbody> ();
		foreach (Rigidbody rb in children) {
			rb.AddForce (direction * force);
		}
	}

	private void orient ()
	{
		Vector3 velocity = rootRigidBody.velocity;
		float magnitude = velocity.magnitude * velocity.y;

		if (Mathf.Abs (magnitude) > 7.0f) {
			Vector3 lookDirection = rootRigidBody.position + velocity;
			Quaternion newRotation = Quaternion.LookRotation (lookDirection, Vector3.up);

			// this value x is 90degrees off
			Quaternion fixedRotation = newRotation * Quaternion.Euler (90, 0, 0);
			rootRigidBody.transform.rotation = Quaternion.Slerp (rootRigidBody.transform.rotation, fixedRotation, Time.fixedTime);
		}
	}
}
