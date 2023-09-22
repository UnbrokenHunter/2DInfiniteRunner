using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supersized : MonoBehaviour
{
    [SerializeField] private float xScaleAmt = 2f;

    void Start()
    {
        if (GameState.Instance.gamemode == Gamemodes.Supersized)
        {
            transform.localScale = new Vector3 (xScaleAmt, transform.localScale.y, transform.localScale.z);
        }
    }

}
