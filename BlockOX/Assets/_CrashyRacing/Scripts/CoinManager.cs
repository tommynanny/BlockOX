using UnityEngine;
using System;
using System.Collections;


public class CoinManager : MonoBehaviour
{
	public static CoinManager instance;

	public int Coins
	{
		get { return _coins; }
		private set { _coins = value; if (value < 0) _coins = 0; }
	}

	public static event Action<int> CoinsUpdated = delegate { };

	[SerializeField]
	[Tooltip("When Load Error, or start of new game")] int DefaultInitialCoins = 0;

	// Show the current coins value in editor for easy testing
	[SerializeField]
	int _coins;

	// key name to store high score in PlayerPrefs
	const string PPK_COINS = "BLOCKOX_COINS";


	void Awake()
	{
		if (instance)
		{
			DestroyImmediate(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void Start()
	{
		Reset();
	}

	public void Reset()
	{
		// Initialize coins
		Coins = SPrefs.GetInt(PPK_COINS, DefaultInitialCoins);
	}

	public void AddCoins(int amount)
	{
		Coins += amount;
		Save();
		CoinsUpdated(Coins);
	}

	public void RemoveCoins(int amount)
	{
		Coins -= amount;
		Save();
		CoinsUpdated(Coins);
	}

	public void Save()
	{
		SPrefs.SetInt(PPK_COINS, Coins);
	}
}

