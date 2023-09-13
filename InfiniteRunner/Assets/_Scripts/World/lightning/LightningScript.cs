using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{

    [SerializeField] private GameObject _textObject;
    [SerializeField] private AudioPlayer _audioPlayer;
    [SerializeField] private Vector3 _offset;

    [Space]

    [SerializeField] private string textPopup;
    [SerializeField] private float invincableTime = 5f;
    [SerializeField] private float speed = 20;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject rect = Instantiate(_textObject);
            rect.transform.position = transform.position + _offset;
            rect.GetComponentInChildren<TextMesh>().text = textPopup;

            _audioPlayer.PlayClip();

			PlayerController player = other.gameObject.GetComponentInParent<PlayerController>();

            player.StartBonus(speed, invincableTime);

            Destroy(transform.parent.gameObject);
        }
        else
        {
            print("Not Player");
            Destroy(transform.parent.gameObject);
        }
    }
}
