using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RespawnTimer : MonoBehaviour
{

	[SerializeField] private float time = .1f;
	[SerializeField] private float itterations = 30;

	[SerializeField] private Image _greyImage;

	[SerializeField] private GameObject deathScreen;

	[SerializeField] private UnityEvent Respawn;

	[SerializeField] private int[] respawnCosts = { 1, 2, 4, 5 };
	private int timesUpgraded = 0;

	private bool willRespawn = false;

	public void SetWillRespawn()
	{
		willRespawn = true;
	}

	private int Cost()
	{
		var minMax = Math.Clamp(timesUpgraded, 0, respawnCosts.Length);
		var cost = respawnCosts[minMax];
		return cost;
	}
	public void TriggerRespawn()
	{
		if (HighScore.Instance.CheckCoinCount() >= Cost())
		{
			HighScore.Instance.UseCoin(Cost());
			timesUpgraded++;
			Respawn.Invoke();
			willRespawn = false;
		}
	}


	private void OnEnable()
	{
		StartCoroutine(RespawnWait());
		GetComponentInChildren<TMP_Text>().text = Cost() + " Coin to Respawn";
	}

	private IEnumerator RespawnWait()
	{
		var wait = new WaitForSecondsRealtime(time);
		for (int i = 0; i < itterations; i++) 
		{
			_greyImage.fillAmount = i / itterations + time; 
			yield return wait; 
		}

		if (!willRespawn)
			deathScreen.SetActive(true);
	}

}
