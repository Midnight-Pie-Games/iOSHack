using UnityEngine;
using System.Collections;

public class TerrainClick : MonoBehaviour {

	Renderer tempRender = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// On Mouse mouse hover change the prefab
	void OnMouseEnter()
	{
//		Debug.Log("TerrainClick: On Mouse Enter");
//		// Show the prefab as a selection.
//		tempRender = renderer;
//		renderer.material.color = Color.white;
	}

	void OnMouseExit()
	{
//		Debug.Log("TerrainClick: On Mouse Exit");
//		// Show the prefab as a selection. 
//		renderer.material.color = tempRender.material.color;
	}

}
