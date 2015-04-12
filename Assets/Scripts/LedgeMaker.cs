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

	private Vector3 startPosition = Vector3.zero;

	private float yOffset = 0;

	//public GameObject.class objectClass;

	// Use this for initialization
	void Start ()
	{
		startPosition = transform.position;

		yOffset = transform.position.y - player.transform.position.y;
		lastEditY = (int)transform.position.y;

		spawnInitials ();
	}

	void spawnInitials ()
	{
		spawnLedgesAt (player.position.y);
		spawnWallsAt (player.position.y);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + yOffset, player.transform.position.z);

		checkAndSpawnLedges ();
	}

	Dictionary<int, HashSet<GameObject>> ledgeMap = new Dictionary<int, HashSet<GameObject>> ();

	void checkAndSpawnLedges ()
	{

		int yPosition = (int)transform.position.y;

		if (Mathf.Abs (yPosition - lastEditY) < HEIGHT) {
			return;
		} else if (ledgeMap.ContainsKey (yPosition)) {
			return;
		} else {
			// update last edit y value
			lastEditY = yPosition;

			spawnLedgesAt (yPosition);
			spawnWallsAt (yPosition);
		}

		// destroy below
		//clearLedgesInRange (bottomRange - rangeSize, bottomRange);
		// destroy above
		//clearLedgesInRange (topRange, topRange + rangeSize);

	}

	void spawnLedgesAt (float yPosition)
	{
		HashSet<GameObject> currentLayer = new HashSet<GameObject> ();
		ledgeMap.Add ((int)yPosition, currentLayer);
		
		float xPos = startPosition.x; 
		for (int b = 0; b < GRID_DENSITY; b++) {
			float maxX = xPos + WIDTH;
			float minX = xPos - WIDTH;
			float ledgeX = Random.Range (minX, maxX);
			
			float maxY = yPosition + yOffset + HEIGHT;
			float minY = yPosition + yOffset - HEIGHT;
			float ledgeY = Random.Range (minY, maxY);
			
			Vector3 ledgePosition = new Vector3 (ledgeX, ledgeY, player.position.z);
			GameObject ledge = GameObject.CreatePrimitive (PrimitiveType.Cube);
			ledge.tag = "Environment";
			ledge.transform.position = ledgePosition;
			
			currentLayer.Add (ledge);
		}
	}

	void spawnWallsAt (float yPosition)
	{
		Vector3 scale = new Vector3 (0.1f, startPosition.y * 2.0f, 1f);
		float xPos = startPosition.x;
		float maxX = xPos + WIDTH;
		float minX = xPos - WIDTH;

		GameObject wallMin = GameObject.CreatePrimitive (PrimitiveType.Cube);
		wallMin.transform.position = new Vector3 (minX, yPosition, player.position.z);
		wallMin.transform.localScale = scale;

		GameObject wallMax = GameObject.CreatePrimitive (PrimitiveType.Cube);
		wallMax.transform.position = new Vector3 (maxX, yPosition, player.position.z);
		wallMax.transform.localScale = scale;
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
