using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WastedEffect : MonoBehaviour {
	public AudioClip WastedSound;
	// Use this for initialization
	void PlayWasted()
	{
		SoundManager.instance.PlaySingle(WastedSound);
	}

	void ThrowDeathPanel()
	{
		Player.Code.OpenDeathPanel();
		Destroy(gameObject);
	}

}
