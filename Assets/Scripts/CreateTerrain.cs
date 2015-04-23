using UnityEngine;
using System.Collections;

public class CreateTerrain : MonoBehaviour {

	public int length = 6;
	public int width = 6;
//	public GameObject terrainPrefab = null; 
//
//	public GameObject player = null;
//	public GameObject mob = null;
//	public GameObject wall = null;


	public GameObject grass = null;
	public GameObject player = null;
	public GameObject mob = null;

	public GameObject wall = null;
	public GameObject tree = null;

	public GameObject[] tiles = null;


	// Use this for initialization
	void Start ()
	{
		for(int i = 0; i < length; i++)
		{
			for(int j = 0; j < width; j++)
			{
				if(j == 3 && i >= 3)
				{
					Instantiate(wall, new Vector3(i, j, 0f), Quaternion.identity);
				}
				else
				{
					// Create a tree to test blocking
					if(i == (length - 1) && j == 0)
					{
						Instantiate(tree, new Vector3(i, j, 0f), Quaternion.identity);
					}
					else
					{
						Instantiate(grass, new Vector3(i, j, 0f), Quaternion.identity);
					}
				}
			}
		}

		Instantiate(player, new Vector3(0f, 0f, 0f), Quaternion.identity);
		Instantiate(mob, new Vector3(4f, 4f, 0f), Quaternion.identity);

		//Instantiate(player, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.Euler(new Vector3(0, 90, 0)));
		//Instantiate(mob, new Vector3((5 * 1) + 0.5f, 0.5f, (5 * 1) + 0.5f), Quaternion.Euler(new Vector3(0, 90, 0)));
		//Instantiate(wall, new Vector3((4 * 1) + 0.5f, 1.0f, (4 * 1 ) + 0.5f), Quaternion.Euler(new Vector3(0, 0, 0)));
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
