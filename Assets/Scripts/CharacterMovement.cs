using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
	GameObject lastClicked;
	Ray ray;
	RaycastHit rayHit;
	public float speed = 1.0f;

	Vector3 currentPos;
	Vector3 camCurrentPos;
	Vector3 newPos;
	Vector3 camNewPos;
	
	float startTime;
	float journeyLength;
	
	bool playerIsMoving = false;

	GameObject cameraObj = null;

	private bool facingRight = true;

	private BoxCollider2D boxCollider;
	public LayerMask blockingLayer;

	// Use this for initialization
	void Start ()
	{
		currentPos = gameObject.transform.position;
		newPos = gameObject.transform.position;
		
		cameraObj = GameObject.Find("Main Camera");

		boxCollider = GetComponent<BoxCollider2D>();

		if(cameraObj != null)
		{
			camCurrentPos = cameraObj.transform.position;
			camNewPos = cameraObj.transform.position;			
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!playerIsMoving)
		{
			if(Input.GetMouseButtonDown (0))
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
							currentPos = gameObject.transform.position;
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

			if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				playerIsMoving = true;
				startTime = Time.time;
				currentPos = gameObject.transform.position;
				newPos = new Vector3(currentPos.x - 1.0f, currentPos.y, currentPos.z);
				camNewPos = new Vector3(camCurrentPos.x - 1.0f, camCurrentPos.y, camCurrentPos.z);
				//transform.LookAt(newPos);
				if(facingRight)
				{
					Flip();
				}
			}
			
			if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
				playerIsMoving = true;
				startTime = Time.time;
				currentPos = gameObject.transform.position;
				newPos = new Vector3(currentPos.x, currentPos.y - 1.0f, currentPos.z);
				camNewPos = new Vector3(camCurrentPos.x, camCurrentPos.y - 1.0f, camCurrentPos.z);
				//transform.LookAt(newPos);
			}
	
			if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
				playerIsMoving = true;
				startTime = Time.time;
				currentPos = gameObject.transform.position;
				newPos = new Vector3(currentPos.x, currentPos.y + 1.0f, currentPos.z);
				camNewPos = new Vector3(camCurrentPos.x, camCurrentPos.y + 1.0f, camCurrentPos.z);
				//transform.LookAt(newPos);
			}
	
			if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				playerIsMoving = true;
				startTime = Time.time;
				currentPos = gameObject.transform.position;
				newPos = new Vector3(currentPos.x + 1.0f, currentPos.y, currentPos.z);
				camNewPos = new Vector3(camCurrentPos.x + 1.0f, camCurrentPos.y, camCurrentPos.z);
				//transform.LookAt(newPos);
				if(!facingRight)
				{
					Flip();
				}
			}
		}
		
		if(currentPos != newPos)
		{
			if(CanMove(newPos))
			{
				// Calculate the journey length.
				journeyLength = Vector3.Distance(currentPos, newPos);
				
				var distCovered = (Time.time - startTime) * speed;
				
				// Fraction of journey completed = current distance divided by total distance.
				var fracJourney = distCovered / journeyLength;
				
				// Set our position as a fraction of the distance between the markers.
				transform.position = Vector3.Lerp(currentPos, newPos, fracJourney);
				
				currentPos = gameObject.transform.position;

				
				// Cameras Position change			
				float camJourneyLength = Vector3.Distance(currentPos, newPos);
				var camDistCovered = (Time.time - startTime) * speed;
				var camFracJourney = camDistCovered / camJourneyLength;
				cameraObj.transform.position = Vector3.Lerp(camCurrentPos, camNewPos, camFracJourney);			
				camCurrentPos = cameraObj.transform.position;
			}
			else
			{
				playerIsMoving = false;
				newPos = currentPos; // Otherwise it keeps looping round.
			}
		}
		else
		{
			playerIsMoving = false;
		}
	}

	bool CanMove(Vector3 moveToPos)
	{
		bool result = false;

		boxCollider.enabled = false;
		RaycastHit2D hit = Physics2D.Linecast(currentPos, moveToPos, blockingLayer);
		boxCollider.enabled = true;

		if(hit.transform == null)
		{
			result = true;
		}

		return result;
	}

	void Flip()
	{
		// Switch the way the player is labelled as facing
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
