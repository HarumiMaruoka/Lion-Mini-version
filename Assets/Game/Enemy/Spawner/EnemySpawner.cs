using Lion.CameraUtility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lion.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverWindow;
        [SerializeField] private TextAsset _spawnData;

        private Vector2 TopRight => Camera.main.GetWorldTopRight() + new Vector2(3f, 3f);
        private Vector2 BottomLeft => Camera.main.GetWorldBottomLeft() - new Vector2(3f, 3f);

        private float _totalTime = 0f;

        private float _elapsed = 0f;

        public float RemainingTime => _totalTime - _elapsed;

        private float _spawnIntervalTimer = 0f;
        private float _spawnInterval = 0f;

        private List<Wave> _waves;

        private int _waveIndex = 0;
        private float _waveElapsed = 0f;

        private Wave _currentWave;

        private void Start()
        {
            _waves = SpawnerDataReader.Load(_spawnData);
            CalculateTotalTime();
            if (_waves.Count == 0) return;
            _currentWave = _waves[_waveIndex];
            _spawnInterval = UnityEngine.Random.Range(_currentWave.MinInterval, _currentWave.MaxInterval);
        }

        private void Update()
        {
            var old = _elapsed;
            _elapsed += Time.deltaTime;
            if (old < _totalTime && _elapsed >= _totalTime)
            {
                // Show Result UI
                _gameOverWindow.SetActive(true);
            }
            if (_elapsed >= _totalTime) return;

            _waveElapsed += Time.deltaTime;
            if (_waveElapsed >= _currentWave.Time)
            {
                _waveElapsed = 0f;
                _waveIndex++;
                if (_waveIndex >= _waves.Count) return;
                _currentWave = _waves[_waveIndex];
            }

            _spawnIntervalTimer += Time.deltaTime;
            if (_spawnIntervalTimer >= _spawnInterval)
            {
                SpawnEnemy();
            }
        }

        private void CalculateTotalTime()
        {
            var totalTime = 0f;
            foreach (var wave in _waves)
            {
                totalTime += wave.Time;
            }
            _totalTime = totalTime;
        }

        private void SpawnEnemy()
        {
            _spawnIntervalTimer = 0f;
            _spawnInterval = UnityEngine.Random.Range(_currentWave.MinInterval, _currentWave.MaxInterval);

            var enemy = EnemyManager.Instance.EnemyPool.GetOrCreateEnemy(GetEnemyID(), transform);
            if (enemy == null) return;
            enemy.transform.position = GetRandomPosition();
        }

        private int GetEnemyID()
        {
            var sumRate = 0f;
            foreach (var data in _currentWave.SpawnableEnemyData)
            {
                sumRate += data.Probability;
            }

            var randomRate = UnityEngine.Random.Range(0f, sumRate);
            var currentRate = 0f;
            for (var i = 0; i < _currentWave.SpawnableEnemyData.Count; i++)
            {
                currentRate += _currentWave.SpawnableEnemyData[i].Probability;
                if (randomRate <= currentRate)
                {
                    return _currentWave.SpawnableEnemyData[i].EnemyID;
                }
            }
            return _currentWave.SpawnableEnemyData[_currentWave.SpawnableEnemyData.Count - 1].EnemyID;

        }

        private Vector3 GetRandomPosition()
        {
            // 上辺、下辺、左辺、右辺のどれかにランダムに出現
            var random = UnityEngine.Random.Range(0, 4);
            switch (random)
            {
                case 0: // 上辺
                    return new Vector3(UnityEngine.Random.Range(BottomLeft.x, TopRight.x), TopRight.y, 0f);
                case 1: // 下辺
                    return new Vector3(UnityEngine.Random.Range(BottomLeft.x, TopRight.x), BottomLeft.y, 0f);
                case 2: // 左辺
                    return new Vector3(BottomLeft.x, UnityEngine.Random.Range(BottomLeft.y, TopRight.y), 0f);
                case 3: // 右辺
                    return new Vector3(TopRight.x, UnityEngine.Random.Range(BottomLeft.y, TopRight.y), 0f);
                default: // ここには来ない
                    return Vector3.zero;
            }

        }
    }
}
