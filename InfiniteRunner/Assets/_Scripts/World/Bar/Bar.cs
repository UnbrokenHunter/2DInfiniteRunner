using System;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private GameObject _destroyParticles;

    private void OnCollisionEnter(Collision collision)
    {
	    if (!collision.gameObject.CompareTag("Player")) return;
	    
	    if (!collision.gameObject.GetComponentInParent<PlayerController>().Die())
	    {
		    collision.gameObject.GetComponentInParent<PlayerController>().AddPoint();

		    Instantiate(_destroyParticles, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().DestroyParticleOnFinish();
		    GetComponent<AudioPlayer>().PlayClip();

		    Destroy(transform.parent.gameObject);
	    }
	    else
	    {
		    GetComponent<AudioPlayer>().PlayClip();
		    Destroy(transform.parent.gameObject);
	    }
    }
}