using System.Collections;
using System.Collections.Generic;
using Ez.Pooly;
using UnityEngine;


public class WorldGenerator : MonoBehaviour
{

	// Use this for initialization
	public bool DisableScript = true;
	[System.Serializable]
	public class RoadGenerator
	{
		[Header("Ground")]
		[Tooltip("The first ground")] public Transform GroundGroupA;
		[Tooltip("The second ground")] public Transform GroundGroupB;
		[Tooltip("The single ground")] public Transform singeGround;

		[Header("Road")]
		[Tooltip("The Road Group")] public Transform RoadGroup;
		[Tooltip("The Sing Road")] public Transform singeRoad;

		public float gap = 5f;

		public int getRoadCount()
		{
			return Mathf.CeilToInt(GroundSizeZ / RoadSizeZ);
		}

		float GroundSizeZ { get { return Mathf.CeilToInt(singeGround.GetComponent<Renderer>().bounds.size.z); } }
		float RoadSizeZ { get { return Mathf.CeilToInt(singeRoad.GetComponent<Renderer>().bounds.size.z); } }
		public IEnumerator generateRoadOnGrounds()
		{
			yield return null;
			foreach (Transform ground in new Transform[] { GroundGroupA, GroundGroupB })
			{
				ground.Find("Ground").GetComponent<Renderer>().enabled = false;
				for (int i = 0; i <= getRoadCount() - 1; i++)
				{
					//yield return null;
					Vector3 pos = new Vector3(ground.position.x, ground.position.y, ground.position.z + gap * i);
					Instantiate(RoadGroup, pos, ground.rotation, ground);
				}
			}
		}

	}

	public RoadGenerator roadGenerator;
	private void Awake()
	{
		if (!DisableScript)
		{
			StartCoroutine(roadGenerator.generateRoadOnGrounds());
		}

	}
}