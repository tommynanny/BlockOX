using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{

	public Text CoinCount;
	public Text DistanceCount;
	public Text TotalScore;
	public Button MenuButton;
	public AudioClip NewHighScore;
	public GameObject TheCanvas;
	public GameObject HighScorePrefab;
	public AudioClip CrowdsSound;
	public ClipPlayerUI Playback;

	int count = 0;
	int delta = 0;
	// Use this for initialization
	private void Start()
	{
		//StartCoroutine(AddingCountffect());
		Playback.gameObject.SetActive(false);
		MenuButton.gameObject.SetActive(false);
	}
	IEnumerator AddingCountffect()
	{
		PlaybackTask();
		//coins
		count = 0;
		delta = Mathf.Max((CoinManager.instance.Coins / 1000), 1) * 6;
		SoundManager.instance.CountingFx(true);
		while (count < CoinManager.instance.Coins)
		{
			count += delta;
			CoinCount.text = count.ToString();
			yield return new WaitForSecondsRealtime(0.001f);
		}
		CoinCount.text = CoinManager.instance.Coins.ToString();
		SoundManager.instance.CountingFx(false);

		//distance
		count = 0;
		delta = Mathf.Max((GameManager.instance.Distance / 1000), 1) * 6;
		SoundManager.instance.CountingFx(true);
		while (count < GameManager.instance.Distance)
		{
			count += delta;
			DistanceCount.text = count.ToString();
			yield return new WaitForSecondsRealtime(0.001f);
		}
		DistanceCount.text = GameManager.instance.Distance.ToString();
		SoundManager.instance.CountingFx(false);


		//Score
		GameManager.instance.Score = CoinManager.instance.Coins * GameManager.instance.Distance;
		count = 0;
		delta = Mathf.Max((GameManager.instance.Score / 1000), 1) * 6;
		SoundManager.instance.CountingFx(true);
		while (count < GameManager.instance.Score)
		{
			count += delta;
			TotalScore.text = count.ToString();
			yield return new WaitForSecondsRealtime(0.001f);
		}
		TotalScore.text = GameManager.instance.Score.ToString();
		SoundManager.instance.CountingFx(false);

		SoundManager.instance.PlaySingle(CrowdsSound);

		HandleHightScore();
		GameManager.instance.SaveGameValue();
		MenuButton.gameObject.SetActive(true);
		Debug.Log("Finish Report");
	}

	public void HandleHightScore()
	{
		if (GameManager.instance.Score > SPrefs.GetInt("Score", 0))
		{
			SoundManager.instance.PlaySingle(NewHighScore);

			// Use this for initialization

			GameObject[] gos = GameObject.FindGameObjectsWithTag("HighScore");
			foreach (GameObject go in gos)
				Destroy(go);

			GameObject temp = Instantiate(HighScorePrefab);
			temp.transform.SetParent(TheCanvas.transform, false);
		}
	}

	public void MenuButtonTask()
	{
		SoundManager.instance.CountingFx(false);
		SoundManager.instance.PlayButtonSound(0);
		GameManager.instance.GoToScene(0,true,true,1);
	}

	public void PlaybackTask()
	{
		Playback.gameObject.SetActive(true);
		GameManager.instance.RotateAppear(Playback.transform, 1, null);
		if (GameManager.instance.myClip != null)
		{
			Gif.PlayClip(Playback, GameManager.instance.myClip);
		}
	}
}
