using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : BaseBehaviour
{
	//Drag a reference to the audio source which will play the sound effects.            //Drag a reference to the audio source which will play the music.
	public static SoundManager instance = null; //Allows other scripts to call functions from SoundManager.				
	public float lowPitchRange = .95f; //The lowest a sound effect will be randomly pitched.
	public float highPitchRange = 1.05f;
	public AudioClip[] bgms;
	public AudioSource DriveSound;
	public AudioSource CountingSound;
	AudioSource bgm;
	AudioSource needToPlay;
	AudioSource LevelUp;
	[Range(0.0f, 1f)] public float BGMVolume = 0.5f;
	[Range(0.0f, 1f)] public float SoundFXVolume = 0.5f;
	[Range(0.0f, 1f)] public float CarEngineVolume = 0.5f;
	public bool FadeInProgress = false;
	public AudioClip[] ButtonSounds;
	// Use this for initialization

	void Update()
	{
		bgm.volume = FadeInProgress ? bgm.volume : BGMVolume;

		LevelUp.volume = SoundFXVolume;
		needToPlay.volume = SoundFXVolume;
		CountingSound.volume = SoundFXVolume;
		//DriveSound.volume = CarEngineVolume;	
		if (SceneManager.GetActiveScene().buildIndex != 0)
		{
			DriveSound.volume = Mathf.Min(GameManager.instance.CarSpeed / 35f, CarEngineVolume);
			DriveSound.panStereo = GameManager.instance.CarHeadAngle / 45f;
		}
		else
		{
			DriveSound.volume = 0;
		}

	}

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);

		if (SceneManager.GetActiveScene().buildIndex == 0 || bgm == null)
		{
			InitializeBGM();
		}
		InitializeAudio();

	}

	public void InitializeBGM()
	{
		bgm = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
		bgm.playOnAwake = true;
		bgm.loop = true;
		bgm.clip = bgms[0];
		bgm.Play();
	}

	public void InitializeAudio()
	{
		needToPlay = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
		needToPlay.playOnAwake = false;
		needToPlay.loop = false;

		LevelUp = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
		LevelUp.playOnAwake = false;
		LevelUp.loop = false;
	}

	public void PlaySingle(AudioClip clip)
	{
		try { needToPlay.PlayOneShot(clip); }
		catch (System.Exception) { }
	}

	public void CountingFx(bool order)
	{
		if (order) { CountingSound.Play(); }
		else { CountingSound.Stop(); }
	}

	public void PlayLevelUp(AudioClip clip, float pitch)
	{
		if (pitch < 1) pitch = 1;
		if (pitch > 1.5f) pitch = 1.5f;
		LevelUp.pitch = pitch;
		try { LevelUp.PlayOneShot(clip); }
		catch (System.Exception) { }
	}

	//RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
	public void RandomizeSfx(params AudioClip[] clips)
	{
		//Generate a random number between 0 and the length of our array of clips passed in.
		int randomIndex = Random.Range(0, clips.Length);

		//Choose a random pitch to play back our clip at between our high and low pitch ranges.
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);

		//Set the pitch of the audio source to the randomly chosen pitch.
		needToPlay.pitch = randomPitch;

		//Set the clip to the clip at our randomly chosen index.

		needToPlay.PlayOneShot(clips[randomIndex]);
		//Play the clip.
	}

	public void FadeOut(float time)
	{
		StopAllCoroutines();
		StartCoroutine(FadeBGM(-1, 0, time));
	}

	public void FadeIn(float time)
	{
		StopAllCoroutines();
		StartCoroutine(FadeBGM(1, BGMVolume, time));
	}

	IEnumerator FadeBGM(int direction, float destination, float duration)
	{
		if (direction == -1) FadeInProgress = true;
		float changeDelta = Mathf.Abs(destination - bgm.volume);
		while (Mathf.Abs(bgm.volume - destination) > .05f)
		{
			bgm.volume += direction / 100f;
			yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime / changeDelta * duration);
		}
		bgm.volume = destination;
		if (direction == 1) FadeInProgress = false;
	}

	public void FadeInAndOut(float time, int bgmIndex)
	{
		StopAllCoroutines();
		StartCoroutine(CrossFade(time, bgmIndex));
	}

	IEnumerator CrossFade(float time, int index)
	{
		StartCoroutine(FadeBGM(-1, 0, 1));

		yield return new WaitForSecondsRealtime(time);
		bgm.volume = 0f;
		bgm.clip = bgms[index];
		bgm.Play();
		StartCoroutine(FadeBGM(1, BGMVolume, 1));
	}

	public void PlayButtonSound(int x)
	{
		if (x <= ButtonSounds.Length - 1)
		{
			PlaySingle(ButtonSounds[x]);
		}
	}
}