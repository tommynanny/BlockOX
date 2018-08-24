using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EasyMobile;

public class LaunchCounter : MonoBehaviour
{
	int launchCount;

	void Awake()
	{
		// Check For 'TimesLaunched', Set To 0 If Value Isnt Set (First Time Being Launched)
		launchCount = SPrefs.GetInt("TimesLaunched", 0);

		// After Grabbing 'TimesLaunched' we increment the value by 1
		launchCount = launchCount + 1;

		// Set 'TimesLaunched' To The Incremented Value
		SPrefs.SetInt("TimesLaunched", launchCount);


		if (launchCount == 1)
		{
			GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_FirstTime);
		}

		if (launchCount <= 5) GameServices.ReportAchievementProgress(EM_GameServicesConstants.Achievement_FifthTime, launchCount / 5f);

		// Now I Would Destroy The Script Or Whatever You
		// Want To Do To Prevent It From Running Multiple
		// Times In One Launch Session
		Destroy(this);
	}
}