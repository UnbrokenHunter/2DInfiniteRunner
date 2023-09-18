using System.Collections;
using System.Collections.Generic;
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

	private bool willRespawn = false;

	public void SetWillRespawn()
	{
		willRespawn = true;
	}

	public void TriggerRespawn()
	{
		if (HighScore.Instance.CheckCoinCount() > 0)
		{
			HighScore.Instance.UseCoin(1);
			Respawn.Invoke();
		}
	}


	private void OnEnable()
	{
		StartCoroutine(RespawnWait());
	}

	private IEnumerator RespawnWait()
	{
		var wait = new WaitForSeconds(time);
		for (int i = 0; i < itterations; i++) 
		{
			_greyImage.fillAmount = i / itterations + time; 
			yield return wait; 
		}

		if (!willRespawn)
			deathScreen.SetActive(true);
	}

}
