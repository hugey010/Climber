using UnityEngine;
using System.Collections;

public class CharacterInput : MonoBehaviour
{

	public float BASE_FORCE = 30f;
	public Rigidbody rigidBody;

	private Vector3 startDragPoint;
	private Vector3 endDragPoint;

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			startDragPoint = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp (0)) {
			endDragPoint = Input.mousePosition;
			jump ();
		}
	}

	private void jump ()
	{
		Vector3 dragLine = endDragPoint - startDragPoint;
		float force = dragLine.y * BASE_FORCE;
		Vector3 direction = dragLine.normalized;
		rigidBody.AddForce (direction * force);
	}
}
