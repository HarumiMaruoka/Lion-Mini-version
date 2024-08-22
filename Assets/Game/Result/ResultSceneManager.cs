using Lion.Enemy;
using System;
using UnityEngine;

namespace Lion.Result
{
    public class ResultSceneManager
    {
        private static ResultSceneManager _instance = new ResultSceneManager();
        public static ResultSceneManager Instance => _instance;
        private ResultSceneManager() { }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            EnemyManager.Instance.OnEnemyKilled += Instance.OnEnemyKilled;
        }

        private int _playerLevel;
        public int PlayerLevel => _playerLevel;
        public void SetPlayerLevel(int level)
        {
            _playerLevel = level;
        }

        private int _killCount;
        public int KillCount => _killCount;
        public void OnEnemyKilled(EnemyController _)
        {
            _killCount++;
        }


        public void Clear()
        {
            EnemyManager.Instance.OnEnemyKilled -= Instance.OnEnemyKilled;
            _instance = new ResultSceneManager();
            EnemyManager.Instance.OnEnemyKilled += Instance.OnEnemyKilled;
        }
    }
}