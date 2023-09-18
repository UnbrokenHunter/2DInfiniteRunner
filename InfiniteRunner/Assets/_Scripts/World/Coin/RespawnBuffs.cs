using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBuffs : MonoBehaviour
{
	public static RespawnBuffs instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(this);
		}
	}

	private bool purchasedHeart = false;

	public void BuyHeart()
	{
		if (HighScore.Instance.CheckCoinCount() > 0)
		{
			HighScore.Instance.UseCoin(1);
			purchasedHeart = true;
			print("Heart Purchased");
		}
	}

	private void OnLevelWasLoaded(int level)
	{
		if (purchasedHeart)
		{
			GameObject.FindObjectOfType<PlayerController>().AddHealth();
			purchasedHeart = false;
			print("Heart Used");
		}
		HighScore.Instance.UpdateCoinCount();

	}

}
