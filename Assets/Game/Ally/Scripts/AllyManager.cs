using Lion.Ally.Skill;
using System;
using UnityEngine;

namespace Lion.Ally
{
    public class AllyManager
    {
        private static AllyManager _instance = new AllyManager();
        public static AllyManager Instance
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
        private AllyManager() { }

        private bool _isInitialized = false;

        private void Initialize()
        {
            AllySheet = Resources.Load<AllySheet>("AllySheet");
            AllySheet.Initialize();
            _instance._isInitialized = true;
        }

        public AllySheet AllySheet { get; private set; }
    }
}