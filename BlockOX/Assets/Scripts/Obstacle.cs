using Ez.Pooly;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

	public AudioClip Hit_Sound;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			gameObject.GetComponent<MeshExploder>().Explode();
			GameManager.instance.CarHealth -= GameManager.instance.DamageTable.ObstacleHit;
			SoundManager.instance.PlaySingle(Hit_Sound);
			Pooly.Despawn(gameObject.transform);
		}
	}
}
