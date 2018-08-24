using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour {
	public GameObject TheCanvas;
	public GameObject SettingPanelPrefab;
	// Use this for initialization

	public void OpenSettingPanel()
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag("SettingPanel");
		foreach (GameObject go in gos)
			Destroy(go);

		GameObject temp = Instantiate(SettingPanelPrefab);
		temp.transform.SetParent(TheCanvas.transform, false);
	}
}
