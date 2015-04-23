using UnityEngine;
using System.Collections;


/// <summary>
/// Game manager. This class will control the player movement and the creation of new terrain with the Perlin noise
/// </summary>
using System.Collections.Generic;
using AssemblyCSharp;


public class GameManager : MonoBehaviour
{
	private enum NewTerrainLocation
	{
		None,
		Left,
		Right,
		Top,
		Bottom
	}

	private GameObject cameraObj = null;
	private GameObject playerObj = null;

	// Tiles
	public GameObject player = null;
	public GameObject mob = null;
	public GameObject[] floor = null;
	public Sprite[] livingMobs = null;
	public Sprite[] deadMobs = null;
	private Dictionary<string, MobDetails> mobLocations = new Dictionary<string, MobDetails> ();
	private NewTerrainLocation terrainLoc = NewTerrainLocation.None;

	public float seed = 0f;
	private float noiseScale = 0.05f * 5.5f;


	public int width = 16;
	public int height = 16;


	// Player Movement stuff
	private Vector3 currentPos;
	private Vector3 camCurrentPos;
	private Vector3 newPos;
	private Vector3 camNewPos;
	
	private float startTime;
	private float journeyLength;

	private bool playerIsMoving = false;
	private bool facingRight = false;
	public float speed = 1.0f;

	private BoxCollider2D boxCollider;
	public LayerMask blockingLayer;


	// Use this for initialization
	void Awake ()
	{
		if(seed == 0f)
		{
			seed = Random.Range(0, 999999);
		}

		cameraObj = GameObject.Find("Main Camera");
		if(cameraObj != null)
		{
			camCurrentPos = cameraObj.transform.position;
			camNewPos = cameraObj.transform.position;			
		}


		cameraObj.transform.position = new Vector3(0f, 0f, -5f);

		playerObj = Instantiate(player, Vector3.zero, Quaternion.identity) as GameObject;
		if (playerObj != null)
		{
			currentPos = playerObj.transform.position;
			newPos = playerObj.transform.position;
			boxCollider = playerObj.GetComponent<BoxCollider2D> ();
		}

		PerlinNoiseStartup();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		UserInput();
		
		if(playerIsMoving)
		{
			PlayerMovment();
			
			MobActions();
		}
	}


	private void PerlinNoiseStartup()
	{
		int playAreaW = width / 2; 
		int playAreaH = height / 2;
		
		for(int i = -playAreaW; i < playAreaW; i++)
		{
			for(int j = -playAreaH; j < playAreaH; j++)
			{
				float sample = Mathf.PerlinNoise(Mathf.Floor(seed + i) * noiseScale, Mathf.Floor(seed + j) * noiseScale); // default
				
				// Create a space for the player
				if((i >= -4 && i <= 4) && (j >= -4 && j <= 4))
				{
					Instantiate(floor[0], new Vector3(i, j, 0f), Quaternion.identity);
				}
				else
				{
					if(sample >= 0.4f)
					{
						Instantiate(floor[0], new Vector3(i, j, 0f), Quaternion.identity);
					}
					else
					{
						Instantiate(floor[1], new Vector3(i, j, 0f), Quaternion.identity);
					}
				}
			}
		}
	}

	private void UserInput()
	{
		if (!playerIsMoving)
		{
			if (Input.GetMouseButtonDown (0))
			{
								/*
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(Physics.Raycast(ray, out rayHit))
				{
					lastClicked = rayHit.collider.gameObject;
					if(lastClicked != null)
					{
						if(lastClicked.name.Contains("Grass"))
						{
							playerIsMoving = true;
							//print(lastClicked.name);
							currentPos = playerObj.transform.position;
							Vector3 newLoc = lastClicked.transform.position;
							newPos = new Vector3(newLoc.x, currentPos.y, newLoc.z);
							
							camCurrentPos = cameraObj.transform.position;
							camNewPos = new Vector3(newLoc.x, camCurrentPos.y, newLoc.z);
							
							startTime = Time.time;
						}
					}
				}
*/
			}
			
			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow))
			{
				playerIsMoving = true;
				startTime = Time.time;
				currentPos = playerObj.transform.position;
				newPos = new Vector3 (currentPos.x - 1.0f, currentPos.y, currentPos.z);
				camNewPos = new Vector3 (camCurrentPos.x - 1.0f, camCurrentPos.y, camCurrentPos.z);
				if (facingRight)
				{
					Flip ();
				}

				terrainLoc = NewTerrainLocation.Left;
			}
	
			if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow))
			{
				playerIsMoving = true;
				startTime = Time.time;
				currentPos = playerObj.transform.position;
				newPos = new Vector3 (currentPos.x, currentPos.y - 1.0f, currentPos.z);
				camNewPos = new Vector3 (camCurrentPos.x, camCurrentPos.y - 1.0f, camCurrentPos.z);
				terrainLoc = NewTerrainLocation.Bottom;
			}
	
			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow))
			{
				playerIsMoving = true;
				startTime = Time.time;
				currentPos = playerObj.transform.position;
				newPos = new Vector3 (currentPos.x, currentPos.y + 1.0f, currentPos.z);
				camNewPos = new Vector3 (camCurrentPos.x, camCurrentPos.y + 1.0f, camCurrentPos.z);
				terrainLoc = NewTerrainLocation.Top;
			}
	
			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow))
			{
				playerIsMoving = true;
				startTime = Time.time;
				currentPos = playerObj.transform.position;
				newPos = new Vector3 (currentPos.x + 1.0f, currentPos.y, currentPos.z);
				camNewPos = new Vector3 (camCurrentPos.x + 1.0f, camCurrentPos.y, camCurrentPos.z);
				if (!facingRight)
				{
					Flip ();
				}
				terrainLoc = NewTerrainLocation.Right;
			}
		}
	}

	public void PlayerMovment()
	{
		GameObject attackedMob = null;
		if(currentPos != newPos)
		{
			if(CanMove(newPos, out attackedMob))
			{
				// Calculate the journey length.
				journeyLength = Vector3.Distance(currentPos, newPos);
				
				var distCovered = (Time.time - startTime) * speed;
				
				// Fraction of journey completed = current distance divided by total distance.
				var fracJourney = distCovered / journeyLength;
				
				// Set our position as a fraction of the distance between the markers.
				playerObj.transform.position = Vector3.Lerp(currentPos, newPos, fracJourney);
				
				currentPos = playerObj.transform.position;
				
				
				// Cameras Position change			
				float camJourneyLength = Vector3.Distance(currentPos, newPos);
				var camDistCovered = (Time.time - startTime) * speed;
				var camFracJourney = camDistCovered / camJourneyLength;
				cameraObj.transform.position = Vector3.Lerp(camCurrentPos, camNewPos, camFracJourney);			
				camCurrentPos = cameraObj.transform.position;
			}
			else
			{
				if(playerIsMoving)
				{
					if(attackedMob != null)
					{
						Vector3 mobLoc = attackedMob.transform.position;
						// Player is attacking a mob
						Debug.Log("Attacked Mob: " + attackedMob.name + "\nX,Y Location: " + mobLoc.x + "," + mobLoc.y);
						string location = mobLoc.x + "," + mobLoc.y;

						SpriteRenderer sr = attackedMob.GetComponent<SpriteRenderer>();
						Mob mobScript = attackedMob.GetComponent<Mob>();

						if(!mobScript.isDead)
						{
							mobScript.Hit(6.0f);

							if(mobScript.isDead)
							{
								mobLocations[location].IsDead = true;
								sr.sprite = deadMobs[mobLocations[location].SpriteIndex];
								sr.sortingLayerName = "Items";
								attackedMob.layer = LayerMask.NameToLayer("Default");
							}
						}
					}

					// Delay it a second?
					//System.Threading.Thread.Sleep(1000);
				}

				playerIsMoving = false;
				newPos = currentPos; // Otherwise it keeps looping round.
			}
		}
		else
		{
			if(playerIsMoving)
			{
				AddMoreTerrain();
				terrainLoc = NewTerrainLocation.None;
			}

			playerIsMoving = false;
		}
	}

	private bool CanMove(Vector3 moveToPos, out GameObject attackedMob)
	{
		bool result = false;
		attackedMob = null;
		
		boxCollider.enabled = false;
		RaycastHit2D hit = Physics2D.Linecast(currentPos, moveToPos, blockingLayer);
		boxCollider.enabled = true;
		
		if(hit.transform == null)
		{
			result = true;
		}
		else
		{
			if(hit.collider.gameObject.name.Equals("Mob(Clone)"))
			{
				attackedMob = hit.collider.gameObject;
			}
		}
		
		return result;
	}
	
	private void Flip()
	{
		// Switch the way the player is labelled as facing
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1
		Vector3 theScale = playerObj.transform.localScale;
		theScale.x *= -1;
		playerObj.transform.localScale = theScale;
	}

	private void AddMoreTerrain()
	{
		int playArea = 0;
		int x = width / 2;
		int y = height / 2;

		bool incrementX = false;
		bool incrementY = false;
		// int playAreaW = width / 2; 
		// int playAreaH = height / 2;

		switch (terrainLoc)
		{
			case NewTerrainLocation.Left:
				playArea = width / 2;
				x = (int)currentPos.x - (width / 2);
				y = (int)currentPos.y - (height / 2);
				incrementY = true;
				break;
			case NewTerrainLocation.Right:
				playArea = width / 2;
				x = (int)(currentPos.x + (width / 2)) - 1;
				y = (int)currentPos.y - (height / 2);
				incrementY = true;
				break;
			case NewTerrainLocation.Top:
				playArea = height / 2;
				x = (int)currentPos.x - (width / 2);
				y = (int)(currentPos.y + (height / 2)) - 1;
				incrementX = true;
				break;
			case NewTerrainLocation.Bottom:
				playArea = height / 2;
				x = (int)currentPos.x - (width / 2);
				y = (int)currentPos.y - (height / 2);
				incrementX = true;
				break;
		}

		for(int f = -playArea; f < playArea; f++)
		{
			float sample = Mathf.PerlinNoise(Mathf.Floor(seed + x) * noiseScale, Mathf.Floor(seed + y) * noiseScale); // default
				
			// Create a space for the player
			if((x >= -4 && x <= 4) && (y >= -4 && y <= 4))
			{
				Instantiate(floor[0], new Vector3(x, y, 0f), Quaternion.identity);
			}
			else
			{
				// This is the Key for finding the Mob object
				string location = x + "," + y;

				if(sample <= 0.6f)
				{
					// Grass
					Instantiate(floor[0], new Vector3(x, y, 0f), Quaternion.identity);

					if(sample <= 0.1f)
					{
						if(!mobLocations.ContainsKey(location))
						{
							MobDetails mobDetails = new MobDetails(); 
							mobDetails.SpriteIndex = Random.Range(0, livingMobs.Length);
							mobDetails.IsDead = false;
							mobDetails.SightDistance = 3.5f;
							mobLocations.Add(location, mobDetails);
						}
					}
				}
				else
				{
					Instantiate(floor[1], new Vector3(x, y, 0f), Quaternion.identity);
				}

				if(mobLocations.ContainsKey(location))
				{
					// Mob aleady exists
					MobDetails currentMob = mobLocations[location];
					//GameObject mobObject = mobGameObject;
					
					//SpriteRenderer sr = mob.GetComponent<SpriteRenderer>();
					Mob mobScript = mob.GetComponent<Mob>();

					mobScript.isDead = currentMob.IsDead;
					mobScript.sightDistance = currentMob.SightDistance;

					if(mobScript.isDead)
					{
						// Dead Mob sprite
						GameObject go = Instantiate(mob, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

						go.layer = LayerMask.NameToLayer("Default");
						SpriteRenderer goSR = go.GetComponent<SpriteRenderer>();
						goSR.sprite = deadMobs[currentMob.SpriteIndex];
						goSR.sortingLayerName = "Items";
					}
					else
					{
						// Living Mob
						GameObject go = Instantiate(mob, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

						SpriteRenderer goSR = go.GetComponent<SpriteRenderer>();
						goSR.sprite = livingMobs[currentMob.SpriteIndex];
					}
				}
			}

			if(incrementX)
			{
				x++;
			}

			if(incrementY)
			{
				y++;
			}
		}
	}
	
	public void MobActions()
	{
		GameObject[] goList = GameObject.FindGameObjectsWithTag("Enemy");
		
		foreach(GameObject currentMob in goList)
		{
			Mob mobScript = currentMob.GetComponent<Mob>();
			
			// check for player
			if(playerObj != null && !mobScript.isDead)
			{
				float distance = Vector3.Distance(playerObj.transform.position, currentMob.transform.position);
				
				if(distance <= mobScript.sightDistance)
				{
				}
			}
		
		
			// Stay, Move or attack
		}
	}
}
