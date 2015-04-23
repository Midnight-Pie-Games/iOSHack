using UnityEngine;
using System.Collections;

public class TerrainRemover : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		// Debug.Log("Destroy " + other.gameObject.name);
		if(other.gameObject.transform.parent)
		{
			Destroy(other.gameObject.transform.parent.gameObject);
		}
		else
		{
			Destroy(other.gameObject);
		}
	}
}
