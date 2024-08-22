using Lion.Actor;
using Lion.Save;
using Lion.Weapon;
using System;
using UnityEngine;

namespace Lion.Minion
{
    public class MinionData : ScriptableObject
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int WeaponID { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Sprite ActorSprite { get; private set; }
        [field: SerializeField] public MinionController Prefab { get; private set; }

        private int _count; // 所持数。
        private MinionController _instance;
        private WeaponInstance[] _equipped = new WeaponInstance[4];

        public bool Unlocked => Count > 0;
        public bool IsActive => _instance != null;

        public ExperiencePointsLevelManager LevelManager { get; private set; }
        public Status Status => LevelManager.CurrentStatus;

        public event Action<int> OnCountChanged;
        public event Action<bool> OnUnlockStatusChanged;
        public event Action<bool> OnActiveChanged;

        public int Count
        {
            get => _count;
            set
            {
                if (_count == 0 && value > 0)
                {
                    OnUnlockStatusChanged?.Invoke(true);
                }
                else if (_count > 0 && value == 0)
                {
                    OnActiveChanged?.Invoke(false);
                }

                _count = value;
                OnCountChanged?.Invoke(value);
            }
        }

        public void Initialize()
        {
            Count = 0;
            LevelManager = new ExperiencePointsLevelManager($"LevelByStatus_Minion{ID}");
            _equipped[0] = WeaponInstance.Create(WeaponID);
        }

        public void Activate()
        {
            _instance = GameObject.Instantiate(Prefab);
            _instance.MinionData = this;
            for (int i = 0; i < _equipped.Length; i++)
            {
                _equipped[i]?.Activation(_instance);
            }

            OnActiveChanged?.Invoke(true);
        }

        public void Deactivate()
        {
            GameObject.Destroy(_instance.gameObject);
            _instance = null;
            ClearWeapon();
            OnActiveChanged?.Invoke(false);
        }

        private void ClearWeapon()
        {
            foreach (var weapon in _equipped)
            {
                weapon?.Deactivation();
            }
        }

        public WeaponInstance Equipped(int index)
        {
            return _equipped[index];
        }

        public void Equip(WeaponInstance weapon, int index)
        {
            if (index < 0 || index >= _equipped.Length)
            {
                Debug.LogWarning("Index is out of range.");
                return;
            }

            _equipped[index]?.Deactivation();
            _equipped[index] = weapon == _equipped[index] ? null : weapon;
            _equipped[index]?.Activation(_instance);
        }
    }
}