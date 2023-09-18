using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetHighScore : MonoBehaviour
{

	[SerializeField] private TMP_Text _highScoreText;

	private void OnEnable()
	{
		_highScoreText.text = "Highscore: " + HighScore.Instance.GetScore() + "m";
	}
}
