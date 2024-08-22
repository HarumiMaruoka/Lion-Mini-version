using System;
using UnityEngine;

namespace Lion.Stage
{
    public class StageManager
    {
        private static StageManager _instance = new StageManager();
        public static StageManager Instance
        {
            get
            {
                if (!_instance._isInitialized)
                {
                    _instance.Initialize();
                }
                return _instance;
            }
        }
        private StageManager() { }

        private bool _isInitialized = false;

        private void Initialize()
        {
            StageSheet = Resources.Load<StageSheet>("StageSheet");
            StageSheet.Initialize();
            _isInitialized = true;

        }

        private bool _isBattleScene = false;
        public event Action<bool> OnBattleSceneChanged;
        public bool IsBattleScene
        {
            get => _isBattleScene;
            set
            {
                _isBattleScene = value;
                OnBattleSceneChanged?.Invoke(_isBattleScene);
            }
        }

        public StageSheet StageSheet { get; private set; }
    }
}