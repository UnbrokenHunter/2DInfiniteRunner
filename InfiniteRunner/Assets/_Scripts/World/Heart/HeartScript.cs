public class HeartScript : AbilityScript
{
    protected override void Ability(PlayerController player)
    {
        player.AddHealth();
    }

    protected override string TextMesh()
    {
        return "+1 Heart";
    }
}
