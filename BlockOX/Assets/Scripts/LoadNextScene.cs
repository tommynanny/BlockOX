using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNextScene : MonoBehaviour
{
	public AudioClip PlayButtonSound;
	private bool loadScene = false;
	public int LoadingSceneIndex;
	public Text loadingText;
	public ProgressBarPro ValueBar;
	[ReadOnly] public int FadeProgress = 0;
	bool FadeFinish = false;
	// Use this for initialization


	// Update is called once per frame
	public void TriggerButton()
	{
		SoundManager.instance.PlaySingle(PlayButtonSound);
		gameObject.SetActive(true);
		GetComponent<Animator>().Play("LoadScene");
	}

	public void DoLoadNext()
	{
		if (loadScene == false)
		{
			loadScene = true;
			loadingText.text = "Loading...";
			SoundManager.instance.FadeInAndOut(3, 1);
			StartCoroutine(LoadNewScene());
		}
	}

	float progress = 0f;
	IEnumerator LoadNewScene()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync(LoadingSceneIndex);
		async.allowSceneActivation = false;

		while (async.progress < 0.9f)
		{
			progress = Mathf.Clamp01(async.progress / 0.9f);
			yield return null;
		}

		
		StartCoroutine(FadingScene());

		while (!FadeFinish)
		{
			progress = Mathf.Clamp01(async.progress / 0.9f);
			yield return null;
		}

		yield return new WaitForSecondsRealtime(2f);
		async.allowSceneActivation = true;
	}
	IEnumerator FadingScene()
	{
		while (FadeProgress != 10)
		{
			Initiate.Fade(LoadingSceneIndex, Color.white, .7f);
			FadeProgress++;
			yield return new WaitForSeconds(.5f);
		}
		FadeFinish = true;
	}

	private void Update()
	{
		ValueBar.Value = progress;
		loadingText.text = progress * 100f + "%";
	}




}