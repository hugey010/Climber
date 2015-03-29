using UnityEngine;
using System.Collections;

public class CharacterInput : MonoBehaviour
{

	private static float BASE_FORCE = 30f;

	public Rigidbody rigidBody;

	private Vector3 startDragPoint;
	private Vector3 endDragPoint;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
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
