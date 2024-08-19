﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lion.Gold
{
    public class DroppedGoldPool
    {
        public static DroppedGoldPool Instance { get; private set; } = new DroppedGoldPool();
        private DroppedGoldPool()
        {
            _prefab = Resources.Load<DroppedGold>("DroppedGold");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            _activePool.Clear();
            _inactivePool.Clear();
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("DroppedGoldPool is already exist.");
            }
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        [SerializeField]
        private DroppedGold _prefab = default;

        private readonly HashSet<DroppedGold> _activePool = new HashSet<DroppedGold>();
        private readonly Queue<DroppedGold> _inactivePool = new Queue<DroppedGold>();

        public int ActiveCount => _activePool.Count;

        public DroppedGold CreateDroppedGold(IActor collector, Vector3 position, int amount)
        {
            DroppedGold gold;
            if (_inactivePool.Count == 0)
            {
                gold = GameObject.Instantiate(_prefab, position, Quaternion.identity);
                gold.Pool = this;
            }
            else
            {
                gold = _inactivePool.Dequeue();
                gold.gameObject.SetActive(true);
            }

            gold.Initialize(collector, position, amount);
            _activePool.Add(gold);
            return gold;
        }

        public void Deactivate(DroppedGold droppedGold)
        {
            if (_activePool.Remove(droppedGold))
            {
                _inactivePool.Enqueue(droppedGold);
                droppedGold.gameObject.SetActive(false);
            }
        }
    }
}