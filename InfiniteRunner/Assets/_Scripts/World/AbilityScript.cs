using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityScript : MonoBehaviour
{

    [Header("General")] [SerializeField] private bool _destroyOnContact = true;
    [SerializeField] private GameObject _textObject;
    [SerializeField] private AudioPlayer _player;
    [SerializeField] private Vector3 _offset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var rect = Instantiate(_textObject);
            rect.transform.position = transform.position + _offset;
            rect.GetComponentInChildren<TextMesh>().text = TextMesh();

            _player.PlayClip();

            Ability(other.gameObject.GetComponentInParent<PlayerController>());
            
            if (_destroyOnContact)
                Destroy(transform.parent.gameObject);
        }
        else
        {
            print("Not Player");
            Destroy(transform.parent.gameObject);
        }
    }

    protected abstract void Ability(PlayerController player);

    protected abstract string TextMesh();

}
