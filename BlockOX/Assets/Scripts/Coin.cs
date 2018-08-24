using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public bool PoolControlled = true;
	public AudioClip CoinSound;
	GameObject CoinMeter;
	public bool hasCollected = false;
	void Awake()
	{
		CoinMeter = GameObject.FindGameObjectWithTag("CoinMeter");
	}

	IEnumerator OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player" && !hasCollected)
		{
			yield return null;
			SoundManager.instance.PlaySingle(CoinSound);
			hasCollected = true;
			//gameObject.GetComponent<MeshExploder>().Explode();
			//Pooly.Despawn(gameObject.transform);
		}
	}


	private void Update()
	{
		if (hasCollected)
			FlyToMeter();
	}

	float t;
	void FlyToMeter()
	{
		transform.position = Vector3.Lerp(transform.position, GameManager.instance.CoinHitPoint, t);// Time.deltaTime);
		t += 0.5f * Time.deltaTime;
	}

	void OnDespawned()
	{
		hasCollected = false;
	}
}
