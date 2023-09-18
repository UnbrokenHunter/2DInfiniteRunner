using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : AbilityScript
{
	protected override void Ability(PlayerController player)
	{
		HighScore.Instance.AddCoin();
	}

	protected override string TextMesh()
	{
		return "+1 Coin";
	}
}
