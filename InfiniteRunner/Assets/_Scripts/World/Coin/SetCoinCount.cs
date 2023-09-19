using TMPro;
using UnityEngine;

public class SetCoinCount : MonoBehaviour
{

	[SerializeField] private TMP_Text _coinText;
	[SerializeField] private string _coinName = "x";

	private void Start()
	{
		HighScore.Instance.CoinUpdate += UpdateText;
	}

	private void UpdateText()
	{
		_coinText.text = _coinName + HighScore.Instance.CheckCoinCount().ToString();
	}

}
