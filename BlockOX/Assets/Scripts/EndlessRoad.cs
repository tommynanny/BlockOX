using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRoad : MonoBehaviour
{
	[Header("Prefabs")]
	public Transform groundA;
	public Transform groundB;
	enum Ground { GroundA, GroundB };
	float PlayerZ;


	[Header("Grounds Location")]
	[ReadOnly] [SerializeField] int GroundAIndex = -1;
	[ReadOnly] [SerializeField] int GroundBIndex = -1;

	[Header("Player Location")]
	[ReadOnly] [SerializeField] Ground CurrentStandOn;
	[ReadOnly] [SerializeField] int CurrentIndex = 0;

	private void Update()
	{
		PlayerZ = Player.instance.transform.position.z;
		CurrentIndex = GetBlockIndexFromZ(PlayerZ);

		CurrentStandOn = CurrentIndex % 2 == 1 ? Ground.GroundA : Ground.GroundB;

		GroundAIndex = GetBlockIndexFromZ(groundA.transform.position.z);
		GroundBIndex = GetBlockIndexFromZ(groundB.transform.position.z);


		if (!HasGroundAtIndex(CurrentIndex)) // need to assign the one under it foot
		{
			ShowGroundbyIndex(CurrentIndex);
		}

		if (!HasGroundAtIndex(CurrentIndex + 1))
		{
			if (PlayerZ > GetBlockStartPoint(CurrentIndex) + 20)
			{
				ShowGroundbyIndex(CurrentIndex + 1);
			}
		}


	}

	bool HasGroundAtIndex(int x)
	{
		return ((x == GroundAIndex) || (x == GroundBIndex));
	}

	void ShowGroundbyIndex(int x)
	{
		GetGroundByIndex(x).position = new Vector3(0, 0, GetBlockStartPoint(x));
	}

	Transform GetGroundByIndex(int x)
	{
		if (x % 2 == 0)
			return groundB;
		return groundA;
	}

	int GetBlockStartPoint(int x)
	{
		return (1000 * x - 1010);
	}

	int GetBlockEndPoint(int x)
	{
		return (1000 * x - 10);
	}

	int GetBlockIndexFromZ(float z)
	{
		return Mathf.FloorToInt((z + 10) / 1000 + 1);
	}
}