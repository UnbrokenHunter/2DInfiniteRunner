using UnityEngine;

public class PowerScript : AbilityScript
{
    [SerializeField] private float _powerAmt = 50f;
    
    protected override void Ability(PlayerController player)
    {
        player.AddPower(_powerAmt);
    }

    protected override string TextMesh()
    {
        return "+" + _powerAmt + " Power";
    }
}
