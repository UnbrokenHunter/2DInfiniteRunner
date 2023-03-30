using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSpawner : MonoBehaviour
{
    [SerializeField] private float _chanceToSpawn = 100;
    [SerializeField] private Vector2 _minMaxSpawnInterval;
    [SerializeField] private Vector2 _minMaxWidth;
    [SerializeField] private float _offscreenAmount;

    [SerializeField] private GameObject[] _barPrefab;

    [SerializeField] private bool _hasIncreasedDifficulty;
    [SerializeField] private Vector2 _increaseDifficultyAfter;
	[SerializeField] private Vector2 _difficultMinMaxSpawnInterval;

	[SerializeField] private GameObject[] _difficultBars;

    private List<GameObject> _barList = new ();

    private Vector2 _spawnPos;
    private Vector2 _currentPos;
    private int _spawnCount;
    private float _nextInterval = 10;
    private float _increaseDifficultyNumber;

    private void Start()
    {
        _spawnPos = transform.position;
        _increaseDifficultyNumber = Random.Range(_increaseDifficultyAfter.x, _increaseDifficultyAfter.y);
    }

    private void FixedUpdate()
    {
        _currentPos = transform.position;

        if (_currentPos.y - _spawnPos.y >= _nextInterval)
        {
            if (Random.Range(0, 100) > _chanceToSpawn)
                return;

            GameObject bar;
            Vector2 verticalPosition = _currentPos + Vector2.up * _offscreenAmount;

            // Increase Difficulty
            if (_hasIncreasedDifficulty && _spawnCount >= _increaseDifficultyNumber)
            {
                bar = Instantiate(_difficultBars[Random.Range(0, _difficultBars.Length)], verticalPosition, Quaternion.identity);
			}
			else
            {
                bar = Instantiate(_barPrefab[Random.Range(0, _barPrefab.Length)], verticalPosition, Quaternion.identity);
            }


                bar.transform.position += Vector3.right * Random.Range(_minMaxWidth.x, _minMaxWidth.y);
            bar.transform.position += Vector3.back;

			_barList.Add(bar);

            if(_barList.Count > 4)
            {
                Destroy(_barList[0]);
                _barList.RemoveAt(0);
            }

            // Pick next interval
            if (_hasIncreasedDifficulty && _spawnCount >= _increaseDifficultyNumber)
                _nextInterval = Random.Range(_difficultMinMaxSpawnInterval.x, _difficultMinMaxSpawnInterval.y);

            else
				_nextInterval = Random.Range(_minMaxSpawnInterval.x, _minMaxSpawnInterval.y);

			_spawnPos = _currentPos;

            _spawnCount++;
        }

    }

}
