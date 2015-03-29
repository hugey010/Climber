using UnityEngine;
using System.Collections; 

public class CameraFollowCharacter : MonoBehaviour
{

	public Transform player;
	public float zOffset;

	void Start ()
	{
		zOffset = transform.position.z - player.position.z;
	}

	void Update ()
	{
		transform.position = new Vector3 (player.position.x, player.position.y, zOffset + player.position.z); 
	}
}
