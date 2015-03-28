using UnityEngine;
using System.Collections;

public class CameraFollowCharacter : MonoBehaviour {

	public Transform player;

	void Update () {
		transform.position = new Vector3 (player.position.x, player.position.y, this.transform.position.z);
	}
}
