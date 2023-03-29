using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private GameObject _objectToFollow;
    [SerializeField] private bool _xAxis;
    [SerializeField] private bool _yAxis;

    private Vector3 _newPos;

	private void Start()
	{
		_newPos = new (0, 0, transform.position.z);
	}

	private void Update()
    {
        if (_objectToFollow == null) return;

        _newPos.x = _xAxis ? _objectToFollow.transform.position.x : 0;
        _newPos.y = _yAxis ? _objectToFollow.transform.position.y : 0;

		transform.position = _newPos;
    }
}
