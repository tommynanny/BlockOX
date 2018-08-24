using System.Collections;
using System.Collections.Generic;
using Chronos;
using Ez.Pooly;
using UnityEngine;
public class LevelSpawner : BaseBehaviour
{
	public Transform SpawnName;
	public float ShiftSpeed = 1f;
	public int SpawnCount = 5;
	public float SpawnInterval = 1f;
	public float CycleInterval = 5f;
	public float SpawnHeight = .7f;
	public float StartSpawnDistance = 90f;
	Vector3 Offset;
	GameObject Ground;
	float GroundLeftPos;
	float GroundRightPos;
	bool HasArrived = true;
	float destX;
	bool hasSpawnAllCoins = true;
	// Use this for initialization
	void Start ()
	{
		Ground = GameObject.FindGameObjectWithTag ("Ground");
		Offset = new Vector3 (0, SpawnHeight, StartSpawnDistance);
	}

	private void Update ()
	{
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, Offset.y, Player.instance.transform.position.z + Offset.z);
		GroundLeftPos = Ground.transform.position.x - Ground.GetComponent<Renderer> ().bounds.size.x / 2;
		GroundRightPos = Ground.transform.position.x + Ground.GetComponent<Renderer> ().bounds.size.x / 2;

		if (HasArrived)
		{
			destX = Random.Range (GroundLeftPos, GroundRightPos);
			HasArrived = false;
		}
		else
		{
			SideShift ();
		}

		if (hasSpawnAllCoins)
		{
			hasSpawnAllCoins = false;
			StartCoroutine (SpawnGap (CycleInterval));
		}
	}

	private void SideShift ()
	{
		if (HasArrived)
			return;

		Vector3 ShitDest = new Vector3 (destX, transform.position.y, transform.position.z);
		transform.position = Vector3.MoveTowards (transform.position, ShitDest, ShiftSpeed * Time.deltaTime);

		if (Vector3.Distance (transform.position, ShitDest) <= .01f)
		{
			HasArrived = true;
		}

	}

	IEnumerator SpawnGap (float deltaT)
	{
		yield return time.WaitForSeconds (deltaT);
		StartCoroutine (SpawnCoins (SpawnCount));
	}

	IEnumerator SpawnCoins (int count)
	{
		for (int i = 0; i < count; i++)
		{

			time.Do (
				false, // Repeatable
				delegate () // On forward
				{
					Transform thisOne = Pooly.Spawn (SpawnName, gameObject.transform.position, SpawnName.rotation);
					return thisOne;
				},
				delegate (Transform thisOne) // On backward
				{
					Pooly.Despawn (thisOne);
				}
			);

			yield return time.WaitForSeconds (SpawnInterval);
		}
		hasSpawnAllCoins = true;
	}

}