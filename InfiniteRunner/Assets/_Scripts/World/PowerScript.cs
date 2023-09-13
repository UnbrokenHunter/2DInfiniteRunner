using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerScript : MonoBehaviour
{

    [SerializeField] private GameObject _textObject;
    [SerializeField] private AudioPlayer _player;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private float _powerAmt = 50;

    private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject rect = Instantiate(_textObject);
            rect.transform.position = transform.position + _offset;
            rect.GetComponentInChildren<TextMesh>().text = "+" + _powerAmt + " Power";


            _player.PlayClip();

            other.gameObject.GetComponentInParent<PlayerController>().AddPower(_powerAmt);
            Destroy(transform.parent.gameObject);
        }
        else
        {
            print("Not Player");
            Destroy(transform.parent.gameObject);
        }
    }


}
