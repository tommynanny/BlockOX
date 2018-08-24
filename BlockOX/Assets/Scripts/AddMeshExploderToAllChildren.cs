using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMeshExploderToAllChildren : MonoBehaviour
{

	void Awake ()
	{
		var allMeshRenderers = GetComponentsInChildren<MeshRenderer> ();
		foreach (var i in allMeshRenderers)
		{
			try
			{
				MeshExploder a = i.gameObject.AddComponent<MeshExploder> ();
				a.type = MeshExploder.ExplosionType.Visual;
			}
			catch (System.Exception) { }
		}
	}

	public GameObject[] ExplodeAll ()
	{
		var allMeshRenderers = GetComponentsInChildren<MeshRenderer> ();
		List<GameObject> result = new List<GameObject> ();
		foreach (var i in allMeshRenderers)
		{
			try
			{
				var one = i.gameObject.GetComponent<MeshExploder> ().Explode ();
				if (one != null)
				{
					result.Add (one);
				}
			}
			catch (System.Exception) { }
		}
		return result.ToArray ();
	}

}