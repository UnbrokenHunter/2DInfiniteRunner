using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetHighScore : MonoBehaviour
{

	[SerializeField] private TMP_Text _text;

	private void OnEnable()
	{
		_text.text = "Highscore: " + HighScore.Instance.GetScore() + "m";
	}
}
