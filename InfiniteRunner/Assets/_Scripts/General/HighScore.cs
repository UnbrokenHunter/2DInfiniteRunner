using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
	public static HighScore Instance;

	public delegate void CoinEvent();  // delegate
	public event CoinEvent CoinUpdate; // event

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public int GetScore() => PlayerPrefs.GetInt("Highscore");

	public void CheckScore(int score)
	{
		if (score > PlayerPrefs.GetInt("Highscore"))
		{
			PlayerPrefs.SetInt("Highscore", score);
		}
	}

	public int CheckCoinCount()
	{
		return PlayerPrefs.GetInt("Coin Count");
	}

	public void UpdateCoinCount()
	{
		CoinUpdate?.Invoke();
	}

	public void AddCoin()
	{
		PlayerPrefs.SetInt("Coin Count", CheckCoinCount() + 1);
		UpdateCoinCount();
	}

	public void UseCoin(int amt)
	{
		PlayerPrefs.SetInt("Coin Count", CheckCoinCount() - amt);
		UpdateCoinCount();
	}
}
