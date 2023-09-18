using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{

	[SerializeField] private bool withHeart = false;

	public void StartGame()
	{
		if (withHeart)
		{
			RespawnBuffs.instance.BuyHeart();
			print("Start with Heart Chosen");
		}

		SceneManager.LoadScene(1);
	}

}
