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



}