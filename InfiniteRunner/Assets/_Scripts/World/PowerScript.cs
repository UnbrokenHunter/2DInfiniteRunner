using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Power"); 
            collision.gameObject.GetComponentInParent<PlayerController>().AddPower();
            Destroy(gameObject);
        }
        else
        {
            print("Not Player");
            Destroy(gameObject);
        }
    }


}
