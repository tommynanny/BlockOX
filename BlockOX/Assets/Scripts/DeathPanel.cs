using System.Collections;
using System.Collections.Generic;
using Chronos;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DeathPanel : MonoBehaviour
{
	public Transform MenuToAppear;
	// Use this for initialization

	private void Start()
	{
		GameManager.instance.PauseAll();
	}

	public void ReviveButton ()
	{
		SoundManager.instance.CountingFx(false);
		gameObject.transform.Find("Background").gameObject.SetActive(false);
		GameManager.instance.RotateDisappear(MenuToAppear, -1, delegate { Timekeeper.instance.Clock("Player").localTimeScale = -5f; GameManager.instance.ResumeAll(); Destroy(gameObject); });
	}

	public void CloseForm()
	{
		GameManager.instance.RotateDisappear(MenuToAppear, -1, delegate {Destroy(gameObject); });
	}

}