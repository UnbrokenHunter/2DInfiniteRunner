using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private GameObject _destroyParticles;

    private void OnCollisionEnter(Collision collision)
	{
        print("Collision");

        if (collision.gameObject.CompareTag("Player"))
        {
            if (!collision.gameObject.GetComponentInParent<PlayerController>().Die())
            {
                Instantiate(_destroyParticles, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().DestroyAudioOnFinish();
                GetComponent<AudioPlayer>().PlayClip();

                Destroy(transform.parent.gameObject);
            }
        }
    }



}