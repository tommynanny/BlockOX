using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
	public Slider BGMSlider;
	public Slider SoundFXSlider;
	public Slider CarEngineSlider;
	public Sprite GreySlider;
	public Sprite GreenSlider;
	public Transform MenuToAppear;

	void Awake()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0)
		{
			gameObject.transform.Find("Menu").Find("ExitButton").transform.Translate(new Vector2(130, 0));
			gameObject.transform.Find("Menu").Find("MenuButton").gameObject.SetActive(false);
			gameObject.transform.Find("Menu").Find("CarEngine Volume").gameObject.SetActive(false);
		}
	}
	// Use this for initialization
	// Update is called once per frame
	void Update()
	{
		SoundManager.instance.BGMVolume = BGMSlider.value;
		SoundManager.instance.SoundFXVolume = SoundFXSlider.value;
		SoundManager.instance.CarEngineVolume = CarEngineSlider.value;

		Slider[] sliders = { BGMSlider, SoundFXSlider, CarEngineSlider };
		foreach (Slider one in sliders)
		{
			if (one.value == 0)
			{
				one.transform.Find("Handle Slide Area").Find("Handle").GetComponent<Image>().sprite = GreySlider;
			}
			else
			{
				one.transform.Find("Handle Slide Area").Find("Handle").GetComponent<Image>().sprite = GreenSlider;
			}
		}
	}

	void Start()
	{
		GameManager.instance.PauseAll();
		BGMSlider.value = SoundManager.instance.BGMVolume;
		SoundFXSlider.value = SoundManager.instance.SoundFXVolume;
		CarEngineSlider.value = SoundManager.instance.CarEngineVolume;

		GameManager.instance.RotateAppear(MenuToAppear, -1, null);
	}



	public void CloseSettingPanelButton()
	{
		gameObject.transform.Find("Background").gameObject.SetActive(false);
		GameManager.instance.RotateDisappear(MenuToAppear, -1, delegate { GameManager.instance.ResumeAll(); Destroy(gameObject); });
	}

	public void MainMenuButton()
	{
		gameObject.transform.Find("Background").gameObject.SetActive(false);
		GameManager.instance.RotateDisappear(MenuToAppear, -1, delegate { GameManager.instance.GoToScene(0, true, true); GameManager.instance.ResumeAll(); Destroy(gameObject); });
	}

	public void ExitButton()
	{
		gameObject.transform.Find("Background").gameObject.SetActive(false);
		GameManager.instance.RotateDisappear(MenuToAppear, -1, delegate { Application.Quit(); GameManager.instance.ResumeAll(); Destroy(gameObject); });
	}

}