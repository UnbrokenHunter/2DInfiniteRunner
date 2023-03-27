using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSpawner : MonoBehaviour
{
    [SerializeField] private float _barInterval;
    [SerializeField] private float _offscreenAmount;

    [SerializeField] private GameObject _barPrefab;

    private Vector2 _spawnPos;
    private Vector2 _currentPos;

    private void Start() => _spawnPos = transform.position;

    private void FixedUpdate()
    {
        _currentPos = transform.position;

        if (_currentPos.y - _spawnPos.y >= _barInterval)
        {
            Instantiate(_barPrefab, _currentPos + Vector2.up * _offscreenAmount, Quaternion.identity);
            _spawnPos = _currentPos;
        }
        
    }

}
