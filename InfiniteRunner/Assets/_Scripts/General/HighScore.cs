using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
	public static HighScore Instance;

	private int _highScore;

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
}
