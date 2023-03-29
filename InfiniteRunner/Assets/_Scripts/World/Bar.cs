using UnityEngine;

public class Bar : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
	{
        print("Collision");

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<PlayerController>().Die();
        }
    }



}