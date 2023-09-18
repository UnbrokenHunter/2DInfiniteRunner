using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
	public static HighScore Instance;

	private int _highScore;
	private int _coinCount;

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

	public int GetScore() => _highScore;

	public void CheckScore(int score)
	{
		if (score > _highScore)
		{
			_highScore = score;
		}
	}

	public int CheckCoinCount()
	{
		return _coinCount;
	}

	public void UpdateCoinCount()
	{
		CoinUpdate?.Invoke();
	}

	public void AddCoin()
	{
		_coinCount++;
		UpdateCoinCount();
	}

	public void UseCoin(int amt)
	{
		_coinCount -= amt;
		UpdateCoinCount();
	}
}
