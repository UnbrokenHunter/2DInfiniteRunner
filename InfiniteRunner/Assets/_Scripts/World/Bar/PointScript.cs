using UnityEngine;
using UnityEngine.Events;

public class PointScript : AbilityScript
{
    [SerializeField] private UnityEvent OnPoint;
    
    protected override void Ability(PlayerController player)
    {
        if (player.IsDead) 
            return;

        OnPoint?.Invoke();
        
        player.AddPoint();
    }

    protected override string TextMesh()
    {
        return "+50m";
    }
}
