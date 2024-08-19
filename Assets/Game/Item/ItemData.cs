using Lion.Save;
using System;
using UnityEngine;

namespace Lion.Item
{
    public class ItemData : ScriptableObject, ISavable
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Price { get; private set; }

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnCountChanged?.Invoke(_count);
            }
        }

        public event Action<int> OnCountChanged;

        public int LoadOrder => -1;

        public void Save()
        {
            PlayerPrefs.SetInt($"Item_{ID}", _count);
        }

        public void Load()
        {
            _count = PlayerPrefs.GetInt($"Item_{ID}", 0);
            OnCountChanged?.Invoke(_count);
        }
    }
}