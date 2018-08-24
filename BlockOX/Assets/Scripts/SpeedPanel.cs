using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPanel : MonoBehaviour
{

	// Use this for initialization
	IEnumerator OnTriggerEnter (Collider other)
	{
		yield return null;
		if (other.tag == "Player")
		{
			for (int i = 1; i <= 1; i++)
			{
				yield return null;
				Player.instance.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 0, 2000), ForceMode.Acceleration);
			}

		}
	}
}