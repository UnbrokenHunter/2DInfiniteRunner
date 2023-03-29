using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerScript : MonoBehaviour
{

    [SerializeField] private GameObject _textObject;
    [SerializeField] private Vector3 _offset;


    private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Player"))
        {
            print("Power");

            GameObject rect = Instantiate(_textObject);
            rect.transform.position = transform.position + _offset;

            other.gameObject.GetComponentInParent<PlayerController>().AddPower();
            Destroy(transform.parent.gameObject);
        }
        else
        {
            print("Not Player");
            Destroy(transform.parent.gameObject);
        }
    }


}
