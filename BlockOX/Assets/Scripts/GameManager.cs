using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Chronos;
using SleekRender;
using EasyMobile;
using System;
using UnityEngine.SocialPlatforms;
//using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{

	#region Singleton
	public static GameManager instance = null;
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);

		Application.targetFrameRate = 60;

		QualitySettings.vSyncCount = 0;

		QualitySettings.antiAliasing = 0;

		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		PostProcessingProfile = Camera.main.GetComponent<SleekRenderPostProcess>().settings;
		PostProcessingProfile.colorize.a = 0;

		StoreReview.RequestRating();
		//if (StoreReview.CanRequestRating())
		//{
		//	StoreReview.RequestRating();
		//}

	}
	#endregion

	#region setting_components
	[Header("Settings")]
	public float PanelOpenDuration = .4f;
	private SleekRenderSettings PostProcessingProfile;

	#endregion

	#region Runtime_Data
	[Header("Runtime")]

	[SerializeField] [ReadOnly] float _CarHealth = 100;
	public float CarHealth { set { _CarHealth = value; if (value < 0) _CarHealth = 0; if (value > 100) _CarHealth = 100; } get { return _CarHealth; } }

	[SerializeField] [ReadOnly] int _Score = 0;
	public int Score { set { _Score = value; if (value < 0) _Score = 0; } get { return _Score; } }

	[ReadOnly] public int Distance;
	[ReadOnly] public float CarSpeed;
	[ReadOnly] public int level;
	[HideInInspector] public float CarHeadAngle;
	[HideInInspector] public Vector3 CoinHitPoint;
	[HideInInspector] public AnimatedClip myClip;
	#endregion

	#region DamgaChart
	public DamgaChart DamageTable;

	[System.Serializable]
	public class DamgaChart
	{
		public float ObstacleHit = 5f;
	}

	#endregion

	#region Personal_Date

	#endregion

	#region Pasue_Resume
	public void PauseAll()
	{
		try
		{
			Time.timeScale = 0f;
			Timekeeper.instance.Clock("Root").localTimeScale = 0f;
		}
		catch { }

	}

	public void ResumeAll()
	{
		try
		{
			Time.timeScale = 1f;
			Timekeeper.instance.Clock("Root").localTimeScale = 1f;
		}
		catch { }

	}
	#endregion

	#region PostProcessing_Color_Change
	public void ColorOut(float time)
	{
		StopAllCoroutines();
		StartCoroutine(ColorChange(1, 255, time));
	}

	public void ColorIn(float time)
	{
		StopAllCoroutines();
		StartCoroutine(ColorChange(-1, 0, time));
	}



	IEnumerator ColorChange(int direction, float destination, float duration)
	{
		float changeDelta = Mathf.Abs(destination - PostProcessingProfile.colorize.a);
		while (Mathf.Abs(PostProcessingProfile.colorize.a - destination) > 5)
		{
			PostProcessingProfile.colorize.a += (byte)(direction * 5);
			yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime / changeDelta * duration);
		}
		PostProcessingProfile.colorize.a = (byte)destination;
		yield return null;
	}
	#endregion

	#region Send_Email
	public void SendEmail(string EmailSubject, string EmailBody, string SenderName, string SenderEmail)
	{
		string email = "NannieSoft@gmail.com";
		string sub = "BlockOX_" + EmailSubject;
		string bod = "Name: " + SenderName + "\n" + "SenderEmail: " + SenderEmail + "\n" + EmailBody;

		string subject = MyEscapeURL(sub);
		string body = MyEscapeURL(bod);
		Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
	}
	private string MyEscapeURL(string url)
	{
		return WWW.EscapeURL(url).Replace("+", "%20");
	}
	#endregion

	#region Rotate_Annimation
	public delegate void Dele();
	public void RotateAppear(Transform one, float duration, Dele todo)
	{
		duration = duration == -1 ? PanelOpenDuration : duration;
		StartCoroutine(Rotate(one, duration, 1, todo));
	}

	public void RotateDisappear(Transform one, float duration, Dele todo)
	{
		duration = duration == -1 ? PanelOpenDuration : duration;
		StartCoroutine(Rotate(one, duration, -1, todo));
	}

	IEnumerator Rotate(Transform one, float duration, int direction, Dele todo)
	{
		if (todo == null) { todo = delegate { }; };
		Vector3 startScale = new Vector3(0, 0, 0);
		Vector3 endScale = new Vector3(0, 0, 0);
		Vector3 endRotation = new Vector3(0, 0, 0);
		Vector3 startRotation = new Vector3(0, 0, 0);
		switch (direction)
		{
			case 1:
				endScale = one.localScale;
				startScale = one.localScale = new Vector3(0, 0, 0);

				endRotation = one.localEulerAngles;
				startRotation = one.localScale = endRotation - new Vector3(0, 0, 360);
				break;

			case -1:
				startScale = one.localScale;
				endScale = new Vector3(0, 0, 0);

				startRotation = one.localScale;
				endRotation = startRotation - new Vector3(0, 0, 360);
				break;
		}


		float t = 0;
		while (t < 1)
		{
			one.eulerAngles = Vector3.Lerp(startRotation, endRotation, t);
			one.localScale = Vector3.Lerp(startScale, endScale, t);
			t += Time.fixedUnscaledDeltaTime / duration;
			yield return new WaitForSecondsRealtime(.0001f);
		}
		one.eulerAngles = endRotation;
		one.localScale = endScale;

		todo();
	}
	#endregion

	#region GameButton_SaveHandle

	public void GoToScene(int Index, bool IntiNewGame = false, bool ChangeBgm = true, float duration = 1f)
	{
		GameManager.instance.ResumeAll();
		if (ChangeBgm) SoundManager.instance.FadeInAndOut(1f, Index);
		if (IntiNewGame) NewGameInit();
		Initiate.Fade(Index, Color.white, 1 / duration);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void NewGameInit()
	{
		CarHealth = 100;
		Distance = 0;
	}
	#endregion

	#region save_game
	public void SaveGameValue()
	{
		CoinManager.instance.Save();
		if (Score > SPrefs.GetInt("Score", 0)) SPrefs.SetInt("Score", Score);

		if (GameServices.IsInitialized())
		{
			GameServices.ReportScore(Score, EM_GameServicesConstants.Leaderboard_LeaderBoard);
		}
	}



	void WriteSavedGame(SavedGame savedGame, byte[] data)
	{
		if (savedGame.IsOpen)
		{
			//	The	saved	game	is	open	and	ready	for	writing
			//	Prepare	the	updated	metadata	of	the	saved	game
			SavedGameInfoUpdate.Builder builder = new SavedGameInfoUpdate.Builder();
			builder.WithUpdatedDescription("New_Description");
			builder.WithUpdatedPlayedTime(TimeSpan.FromMinutes(30));                //	update	the	played	time	to	30	minutes
			SavedGameInfoUpdate infoUpdate = builder.Build();
			GameServices.SavedGames.WriteSavedGameData(
			savedGame,
			data,
			infoUpdate,             //	update	saved	game	properties
			(SavedGame updatedSavedGame, string error) =>
			{
				if (string.IsNullOrEmpty(error))
				{
					Debug.Log("Saved	game	data	has	been	written	successfully!");
				}
				else
				{
					Debug.Log("Writing	saved	game	data	failed	with	error:	" + error);
				}
			}
			);
		}
		else
		{
			//	The	saved	game	is	not	open.	You	can	optionally	open	it	here	and	repeat	the	process.
			Debug.Log("You	must	open	the	saved	game	before	writing	to	it.");
		}
	}
	#endregion
}