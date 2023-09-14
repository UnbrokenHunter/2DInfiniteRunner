using UnityEngine;

public class LightningScript : AbilityScript
{
    [SerializeField] private string textPopup;
    [SerializeField] private float invincableTime = 5f;
    [SerializeField] private float speed = 20;
    
    protected override void Ability(PlayerController player)
    {
        player.StartBonus(speed, invincableTime);
    }

    protected override string TextMesh()
    {
        return textPopup;
    }
}
