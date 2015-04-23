using UnityEngine;
using System.Collections;

public class PerlinLandscape : MonoBehaviour {

	public GameObject grass = null;
	public GameObject tree = null;
	public GameObject player = null;

	GameObject cameraObj = null;

	public int width = 16;
	public int height = 16;

	private float noiseScale = 0.05f * 5.5f;
	public float seed = 0f;

	//bool startPosFound = false;
	Vector3 playerStartLocation = Vector3.zero;
	Vector3 cameraStartLocation = new Vector3(0f, 0f, -5f);

	// Use this for initialization
	void Start ()
	{
		if(seed == 0f)
		{
			seed = Random.Range(0, 999999);
		}

		cameraObj = GameObject.Find("Main Camera");

		PerlinNoise();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PerlinNoise()
	{
//		Color color = new Color();
//		Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
//		renderer.material.mainTexture = texture;
		int playAreaW = width / 2; 
		int playAreaH = height / 2;

		for(int i = -playAreaW; i < playAreaW; i++)
		{
			for(int j = -playAreaH; j < playAreaH; j++)
			{
				//if(Mathf.Round(Random.Range(0.0f, 1.0f)) == 0)

				// float sample = Mathf.PerlinNoise(i, j);
				float sample = Mathf.PerlinNoise(Mathf.Floor(seed + transform.position.x + i) * noiseScale, Mathf.Floor(seed + transform.position.z + j) * noiseScale); // default

				// Create a space for the player
				if((i >= -4 && i <= 4) && (j >= -4 && j <= 4))
				{
					Instantiate(grass, new Vector3(i, j, 0f), Quaternion.identity);
				}
				else
				{
					if(sample >= 0.4f)
					{
	//					color = Color.red;
						Instantiate(grass, new Vector3(i, j, 0f), Quaternion.identity);

	//					if(!startPosFound && (i >= 0 && j >= 0))
	//					{
	//						startPosFound = true;
	//						playerStartLocation = new Vector3(i, j, 0f);
	//						cameraStartLocation = new Vector3(i, j, -5f);
	//					}
					}
					else
					{
	//					color = Color.white;
						Instantiate(tree, new Vector3(i, j, 0f), Quaternion.identity);
					}
				}
//				texture.SetPixel(x, y, color);
			}
		}
//		texture.Apply();

		if(playerStartLocation != null)
		{
			Instantiate(player, playerStartLocation, Quaternion.identity);
			cameraObj.transform.position = cameraStartLocation;
		}
	}

}
