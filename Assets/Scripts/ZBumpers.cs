using UnityEngine;
using System.Collections;

public class ZBumpers : MonoBehaviour
{
	public Rigidbody rigidBody;

	private float startZ;

	void Start ()
	{
		startZ = rigidBody.position.z;
	}

	void Update ()
	{
		rigidBody.MovePosition (new Vector3 (rigidBody.position.x, rigidBody.position.y, startZ));
	}
}
