using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{

    [SerializeField] private GameObject _textObject;
    [SerializeField] private AudioPlayer _audioPlayer;
    [SerializeField] private Vector3 _offset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject rect = Instantiate(_textObject);
            rect.transform.position = transform.position + _offset;

            _audioPlayer.PlayClip();

            other.gameObject.GetComponentInParent<PlayerController>().AddHealth();
            Destroy(transform.parent.gameObject);
        }
        else
        {
            print("Not Player");
            Destroy(transform.parent.gameObject);
        }
    }
}
