using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// position

public class LedgeMaker : MonoBehaviour
{


	public float WIDTH = 10;
	public float HEIGHT = 20;
	public float GRID_DENSITY = 10; // per 100 in either direction

	private int rangeSize = 100;

	public Transform player;

	private int lastEditY;

	private float yOffset = 0;

	//public GameObject.class objectClass;

	// Use this for initialization
	void Start ()
	{
		yOffset = transform.position.y - player.transform.position.y;
		lastEditY = (int)transform.position.y;
	}

	void spawnInitialLedges ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{

		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + yOffset, player.transform.position.z);


		checkAndSpawnLedges ();

		// check if has changed more  than x amount



		// if so, delete and spawn more blocks, on both ends (vertically and horizontally)

	}

	Dictionary<int, HashSet<GameObject>> ledgeMap = new Dictionary<int, HashSet<GameObject>> ();

	//Dictionary<HashSet<IEnumerable<int>, Vector3>> positionsMap = new Dictionary<HashSet<IEnumerable<int>, Vector3>> ();
	void checkAndSpawnLedges ()
	{

		int yPosition = (int)transform.position.y;

		if (Mathf.Abs (yPosition - lastEditY) < HEIGHT) {
			return;
		} else {
			// update last edit y value
			lastEditY = yPosition;
		}

		Debug.Log ("making cube at y = " + yPosition);

		int topRange = yPosition + rangeSize;
		int bottomRange = yPosition - rangeSize;

		// spawn inbetween. y * density
		//for (int i = bottomRange; i < topRange; i++) {
		// spawn density blocks per y value

		// create new hashset for storage
		HashSet<GameObject> currentLayer = new HashSet<GameObject> ();
		ledgeMap.Add (yPosition, currentLayer);

		float xPos = transform.position.x;
		for (int b = 0; b < GRID_DENSITY; b++) {
			float maxX = xPos + WIDTH;
			float minX = xPos - WIDTH;
			float ledgeX = Random.Range (minX, maxX);
			Vector3 ledgePosition = new Vector3 (ledgeX, player.position.y + yOffset, player.position.z);
			GameObject ledge = GameObject.CreatePrimitive (PrimitiveType.Cube);
			ledge.transform.position = ledgePosition;

			currentLayer.Add (ledge);
		}

		// destroy below
		//clearLedgesInRange (bottomRange - rangeSize, bottomRange);
		// destroy above
		//clearLedgesInRange (topRange, topRange + rangeSize);

	}

	void clearLedgesInRange (int bottomRange, int topRange)
	{
		for (int i = bottomRange; i < topRange; i++) {
			if (ledgeMap.ContainsKey (i)) {
				HashSet<GameObject> ledges = ledgeMap [i];
				foreach (GameObject ledge in ledges) {
					Destroy (ledge);
				}
				ledgeMap.Remove (i);
			}
		}
	}
}
