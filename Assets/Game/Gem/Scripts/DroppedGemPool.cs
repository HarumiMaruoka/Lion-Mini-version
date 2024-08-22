using Lion.Actor;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lion.Gem
{
    public class DroppedGemPool
    {
        public static DroppedGemPool Instance { get; private set; } = new DroppedGemPool();
        private DroppedGemPool()
        {
            _prefab = Resources.Load<DroppedGem>("DroppedGem");
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
                Debug.LogWarning("DroppedGemPool is already exist.");
            }
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        [SerializeField]
        private DroppedGem _prefab = default;

        private readonly HashSet<DroppedGem> _activePool = new HashSet<DroppedGem>();
        private readonly Queue<DroppedGem> _inactivePool = new Queue<DroppedGem>();

        public int ActiveCount => _activePool.Count;

        public DroppedGem CreateDroppedGem(IActor collector, Vector3 position, int amount)
        {
            DroppedGem gem;
            if (_inactivePool.Count == 0)
            {
                gem = GameObject.Instantiate(_prefab, position, Quaternion.identity);
                gem.Pool = this;
            }
            else
            {
                gem = _inactivePool.Dequeue();
                gem.gameObject.SetActive(true);
            }
            gem.Initialize(collector, position, amount);

            _activePool.Add(gem);
            return gem;
        }

        public void Deactivate(DroppedGem droppedGem)
        {
            if (_activePool.Remove(droppedGem))
            {
                _inactivePool.Enqueue(droppedGem);
                droppedGem.gameObject.SetActive(false);
            }
        }
    }
}