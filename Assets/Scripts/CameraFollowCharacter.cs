using UnityEngine;
using System.Collections; 

public class CameraFollowCharacter : MonoBehaviour
{

	public Transform player;
	public float zOffset;
	public float yOffset;

	private Vector3 startPosition;

	private CharacterInput characterInputScript = null;

	void Start ()
	{
		characterInputScript = GetComponent<CharacterInput> ();

		zOffset = transform.position.z - player.position.z;
		startPosition = transform.position;
	}

	void Update ()
	{
		transform.position = new Vector3 (startPosition.x, player.position.y + yOffset, zOffset + player.position.z); 

	}
}
