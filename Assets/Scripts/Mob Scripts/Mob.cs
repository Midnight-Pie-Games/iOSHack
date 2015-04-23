using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour
{
	public float sightDistance = 5.0f;

	private GameObject player = null;
	public bool isDead = false;
	public float health = 10f;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find("PlayerSprite(Clone)");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
//		if(player != null && !isDead)
//		{
//			float distance = Vector3.Distance(player.transform.position, transform.position);
//
//			if(distance <= sightDistance)
//			{
				// Player is in range
				// Check Line Of Sight
				//Debug.Log("Player found: Distance: " + distance);
/*
				RaycastHit rayHit;

				var rayDirection = player.transform.position - transform.position;
				Vector3 fwd = transform.TransformDirection(Vector3.forward);

				// If the direction is grater than 90 left of right then the player is out of sight and don't raycast
				float angle = Vector3.Angle(rayDirection, fwd);

				// Some times the angle comes out at 90.00001
				if(angle >= 0.0f && angle <= 90.01f)
				{
					if (Physics.Raycast (transform.position, rayDirection, out rayHit, sightDistance))
					{
						// Problem the raycast hits the Mobs Arm and the player is lost.
						if (rayHit.collider.gameObject.name.Contains("Player"))
						{
							// enemy can see the player!
							Debug.Log("Player found: Angle: " + angle + " Distance: " + distance + " Object name: " + rayHit.collider.gameObject.name);
						}
						else
						{
							// there is something obstructing the view
							Debug.Log("Player cannot be seen: Angle: " + angle + " Distance: " + distance + " Object name: " + rayHit.collider.gameObject.name);
						}
					}
				}
				else
				{
					Debug.Log("Player is at an angle of: " + angle + " Distance: " + distance);
				}
*/				
//			}
//			else
//			{
//				//Debug.Log("Player is out of sight lost. Distance: " + distance);
//			}
//		}
	}
	
	void OnMouseDown()
	{
		Debug.Log("Mob Clicked");
	}

	public void Hit(float damage)
	{
		health -= damage;

		if (health <= 0f)
		{
			isDead = true;
		}
	}
}
