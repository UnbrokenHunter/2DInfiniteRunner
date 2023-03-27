using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private GameObject _objectToFollow;
    [SerializeField] private bool _xAxis;
    [SerializeField] private bool _yAxis;

    private void Update()
    {
        transform.position = new Vector2(_xAxis ? _objectToFollow.transform.position.x : 0, _yAxis ? _objectToFollow.transform.position.y : 0);
    }
}
