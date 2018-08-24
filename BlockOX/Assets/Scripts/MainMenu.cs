using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using System;

public class MainMenu : MonoBehaviour
{
	public Text PersonalCoinText;
	public Text ScoreText;
	public GameObject TheCanvas;
	public GameObject CreditPanelPrefab;

	[Header("Reward System")]
	public GameObject rewardUI;
	public GameObject dailyRewardBtn;
	public Text dailyRewardBtnText;
	Animator dailyRewardAnimator;
	// Use this for initialization
	private void Start()
	{
		dailyRewardAnimator = dailyRewardBtn.GetComponent<Animator>();

	}

	private void Update()
	{
		dailyRewardBtn.gameObject.SetActive(!DailyRewardController.instance.disable);
		//update coin
		PersonalCoinText.text = "x " + CoinManager.instance.Coins.ToString();
		ScoreText.text = "Socre: " + SPrefs.GetInt("Score", 0).ToString();
		//reward
		if (!DailyRewardController.instance.disable && dailyRewardBtn.gameObject.activeInHierarchy)
		{
			dailyRewardBtn.SetActive(true);
			if (DailyRewardController.instance.CanRewardNow())
			{
				dailyRewardBtnText.text = "GRAB YOUR REWARD!";
				dailyRewardAnimator.SetBool("canGrab", true);
				dailyRewardBtnText.color = new Color(241 / 255f, 145 / 255f, 0 / 255f);
			}
			else
			{
				TimeSpan timeToReward = DailyRewardController.instance.TimeUntilReward;
				dailyRewardBtnText.text = string.Format("REWARD IN {0:00}:{1:00}:{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
				dailyRewardAnimator.SetBool("canGrab", false);
				dailyRewardBtnText.color = new Color(148 / 255f, 128 / 255f, 0 / 255f);
			}
		}

	}

	public void PlayTheGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LoadLeaderboard()
	{
		if (GameServices.IsInitialized())
		{
			GameServices.ShowLeaderboardUI(EM_GameServicesConstants.Leaderboard_LeaderBoard);
		}
		else
		{
#if UNITY_ANDROID
			GameServices.Init();                //	start	a	new	initialization	process
#elif UNITY_IOS
Debug.Log("Cannot	show	leaderboard	UI:	The	user	is	not	logged	in	to	Game	Center.");
#endif
		}
	}

	public void AchiveButton()
	{
		if (GameServices.IsInitialized())
		{
			GameServices.ShowAchievementsUI();
		}
		else
		{
#if UNITY_ANDROID
			GameServices.Init();                //	start	a	new	initialization	process
#elif UNITY_IOS
Debug.Log("Cannot	show	achievements	UI:	The	user	is	not	logged	in	to	Game	Center.");
#endif
		}
	}

	#region subscrib
	void OnEnable()
	{
		GameServices.UserLoginSucceeded += OnUserLoginSucceeded;
		GameServices.UserLoginFailed += OnUserLoginFailed;
	}
	//	Unsubscribe
	void OnDisable()
	{
		GameServices.UserLoginSucceeded -= OnUserLoginSucceeded;
		GameServices.UserLoginFailed -= OnUserLoginFailed;
	}
	#endregion
	//	Event	handlers
	void OnUserLoginSucceeded()
	{
		Debug.Log("User	logged	in	successfully.");
	}
	void OnUserLoginFailed()
	{
		Debug.Log("User	login	failed.");
	}

	// Use this for initialization

	public void OpenCreditPanelButtons()
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag("CreditPanel");
		foreach (GameObject go in gos)
			Destroy(go);

		GameObject temp = Instantiate(CreditPanelPrefab);
		temp.transform.SetParent(TheCanvas.transform, false);
	}

	public void GrabDailyReward()
	{
		if (DailyRewardController.instance.CanRewardNow())
		{
			int reward = DailyRewardController.instance.GetRandomReward();

			// Round the number and make it mutiplies of 5 only.
			int roundedReward = (reward / 5) * 5;

			// Show the reward UI
			ShowRewardUI(roundedReward);

			// Update next time for the reward
			DailyRewardController.instance.ResetNextRewardTime();
		}
	}

	public void ShowRewardUI(int reward)
	{
		rewardUI.SetActive(true);
		rewardUI.GetComponent<RewardUIController>().Reward(reward);
	}

	public void HideRewardUI()
	{
		rewardUI.GetComponent<RewardUIController>().Close();
	}
}
