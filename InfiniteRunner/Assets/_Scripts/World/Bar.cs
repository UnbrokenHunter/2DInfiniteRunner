using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bar : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision");

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
    }



}