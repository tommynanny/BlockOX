using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public Recorder recorder;
	[Range(1, 10)] [SerializeField] int LevelUpDifficulty = 1;
	int level = 0;
	public AudioClip LevelUpSound;
	[ReadOnly]
	[SerializeField]


	public static LevelManager instance = null;
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}

	private void Start()
	{
		Gif.StartRecording(recorder);
	}

	public void StopRecord()
	{
		GameManager.instance.myClip = Gif.StopRecording(recorder);
	}
	private void Update()
	{
		level = getCurrentLevel(CoinManager.instance.Coins);
		GameManager.instance.level = level;
	}

	public int getCurrentLevel(int Coin)
	{
		return Mathf.FloorToInt(Mathf.Sqrt(Coin) / LevelUpDifficulty);
	}

	public int CurrentLevelRequiredXp()
	{

		return (level) * LevelUpDifficulty * (level) * LevelUpDifficulty;
	}

	public int NextLevelRequiredXp()
	{
		return (level + 1) * LevelUpDifficulty * (level + 1) * LevelUpDifficulty;
	}

	public int LevelUpRequiredTotalXP()
	{
		return NextLevelRequiredXp() - CurrentLevelRequiredXp();
	}

	public float getXpBarValue()
	{
		return (CoinManager.instance.Coins - CurrentLevelRequiredXp()) * 1.0f / LevelUpRequiredTotalXP();
	}

	public void PlayLevelUpSound()
	{
		SoundManager.instance.PlayLevelUp(LevelUpSound, 1 + level * 0.01f);
	}

}