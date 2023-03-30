using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointScript : MonoBehaviour
{
    [SerializeField] private UnityEvent OnPoint;

    private void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("Player"))
        {
            if (other.GetComponentInParent<PlayerController>().IsDead) 
                return;

            OnPoint?.Invoke();

            other.GetComponentInParent<PlayerController>().AddPoint();
        }
    }
}
