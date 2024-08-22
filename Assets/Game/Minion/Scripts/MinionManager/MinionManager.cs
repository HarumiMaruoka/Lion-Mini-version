using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lion.Minion
{
    public class MinionManager
    {
        private static MinionManager _instance = new MinionManager();
        public static MinionManager Instance
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
        private MinionManager() { }

        private bool _isInitialized = false;

        private void Initialize()
        {
            MinionSheet = Resources.Load<MinionSheet>("MinionSheet");
            MinionSheet.Initialize();
            _instance._isInitialized = true;
        }

        public MinionSheet MinionSheet { get; private set; }
    }
}