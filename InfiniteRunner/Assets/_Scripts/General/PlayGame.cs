using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{

	[SerializeField] private bool withHeart = false;
	[SerializeField] private Gamemodes gamemode;

	[SerializeField] private bool useCurrentGamemode = false;

	public void StartGame()
	{
		if (withHeart)
		{
			RespawnBuffs.instance.BuyHeart();
			print("Start with Heart Chosen");
		}

		if (!useCurrentGamemode) 
			GameState.Instance.gamemode = gamemode;

		SceneManager.LoadScene(1);
	}

}
