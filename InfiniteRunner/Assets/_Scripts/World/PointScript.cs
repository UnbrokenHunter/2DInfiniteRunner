using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointScript : MonoBehaviour
{
    [SerializeField] private UnityEvent OnPoint;

    [SerializeField] private Vector3 _offset;
    [SerializeField] private GameObject _textObject;

    private void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("Player"))
        {
            if (other.GetComponentInParent<PlayerController>().IsDead) 
                return;

            OnPoint?.Invoke();

			GameObject text = Instantiate(_textObject);
			text.transform.position = transform.position + _offset;

			other.GetComponentInParent<PlayerController>().AddPoint();
        }
    }
}
