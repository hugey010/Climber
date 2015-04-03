using UnityEngine;
using System.Collections;

public class CharacterInput : MonoBehaviour
{

	public float BASE_FORCE = 0.01f;
	public Rigidbody rootRigidBody;
	private float VELOCITY_BASE = 0.001f;

	private Vector3 startDragPoint;
	private Vector3 endDragPoint;
	private float zPosition;



	void Start ()
	{
		zPosition = rootRigidBody.position.z;

		//InvokeRepeating ("orient", 0f, 0.5f);
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

		rootRigidBody.velocity = direction * force * VELOCITY_BASE;

		/*
		rootRigidBody.AddForce (direction * force);
		Rigidbody[] children = rootRigidBody.GetComponentsInChildren<Rigidbody> ();
		foreach (Rigidbody rb in children) {
			rb.AddForce (direction * force);
		}
		*/
	}


	private void orient ()
	{
		Vector3 velocity = rootRigidBody.velocity;
		//Debug.Log ("velocity = " + velocity);

		float magnitude = velocity.magnitude * velocity.y;
		Vector3 lookDirection = rootRigidBody.position + velocity;

		/*
		float vx = velocity.normalized.x;
		float vy = velocity.normalized.y;



		float tiltAroundX = Mathf.Acos (vx / vy) * Mathf.Rad2Deg;
		if (float.IsNaN (tiltAroundX)) {
			tiltAroundX = 0f;
		}

		Debug.Log ("x rotate = " + tiltAroundX);


		float tiltAroundY = 0f;
		if (tiltAroundX > 0f && tiltAroundX < 180) {
			tiltAroundY = 90f;
		} else {
			tiltAroundY = 270f;
		}

		//Debug.Log 



		Quaternion target = Quaternion.Euler (xcounter++, tiltAroundY, 0f);

		Debug.Log ("target rotate = " + target);
		rootRigidBody.rotation = target;

		*/




		Quaternion newRotation = Quaternion.LookRotation (lookDirection, Vector3.up);

		Quaternion fixedRotation = newRotation * Quaternion.Euler (90f, 0f, 0f);
		rootRigidBody.transform.rotation = fixedRotation; 	

		transform.position = new Vector3 (transform.position.x, transform.position.y, zPosition);
		/*
		Vector3 velocity = rootRigidBody.velocity;
		float magnitude = velocity.magnitude * velocity.y;

		if (Mathf.Abs (magnitude) > 7.0f) {
			Vector3 lookDirection = rootRigidBody.position + velocity;
			Quaternion newRotation = Quaternion.LookRotation (lookDirection, Vector3.up);

			// this value x is 90degrees off
			Quaternion fixedRotation = newRotation * Quaternion.Euler (90, 0, 0);
			rootRigidBody.transform.rotation = Quaternion.Slerp (rootRigidBody.transform.rotation, fixedRotation, Time.fixedTime);
		}
		*/
	}
}
