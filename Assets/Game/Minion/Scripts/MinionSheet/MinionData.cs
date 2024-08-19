﻿using Lion.Formation;
using Lion.LevelManagement;
using Lion.Save;
using Lion.Weapon;
using System;
using UnityEngine;

namespace Lion.Minion
{
    public class MinionData : ScriptableObject, IWeaponEquippable, ISavable
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Sprite ActorSprite { get; private set; }
        [field: SerializeField] public MinionController Prefab { get; private set; }

        private int _count; // 所持数。
        private MinionController _instance;
        private WeaponInstance[] _equipped = new WeaponInstance[4];

        public bool Unlocked => Count > 0;
        public bool IsActive => _instance != null;
        public MinionLevelManager LevelManager { get; private set; }
        public MinionStatus Status => LevelManager.Status;

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
            LevelManager = new MinionLevelManager(this);
            SaveManager.Instance.Register(this);
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

        public int LoadOrder => 1;

        public void Save()
        {
            // カウント、レベル、装備を保存する。
            var count = Count;
            var itemLevel = LevelManager.ItemLevelManager.CurrentLevel;
            var exp = LevelManager.ExpLevelManager.CurrentExp;

            var equippedWeapon0 = WeaponManager.Instance.Inventory.IndexOf(_equipped[0]);
            var equippedWeapon1 = WeaponManager.Instance.Inventory.IndexOf(_equipped[1]);
            var equippedWeapon2 = WeaponManager.Instance.Inventory.IndexOf(_equipped[2]);
            var equippedWeapon3 = WeaponManager.Instance.Inventory.IndexOf(_equipped[3]);

            PlayerPrefs.SetInt($"Minion_{ID}_Count", count);
            PlayerPrefs.SetInt($"Minion_{ID}_ItemLevel", itemLevel);
            PlayerPrefs.SetInt($"Minion_{ID}_ExpLevel", exp);

            PlayerPrefs.SetInt($"Minion_{ID}_Equipped0", equippedWeapon0);
            PlayerPrefs.SetInt($"Minion_{ID}_Equipped1", equippedWeapon1);
            PlayerPrefs.SetInt($"Minion_{ID}_Equipped2", equippedWeapon2);
            PlayerPrefs.SetInt($"Minion_{ID}_Equipped3", equippedWeapon3);
        }

        public void Load()
        {
            // カウント、レベル、装備をロードする。
            var count = PlayerPrefs.GetInt($"Minion_{ID}_Count", 0);
            var itemLevel = PlayerPrefs.GetInt($"Minion_{ID}_ItemLevel", 1);
            var exp = PlayerPrefs.GetInt($"Minion_{ID}_ExpLevel", 0);

            var equippedWeapon0 = PlayerPrefs.GetInt($"Minion_{ID}_Equipped0", -1);
            var equippedWeapon1 = PlayerPrefs.GetInt($"Minion_{ID}_Equipped1", -1);
            var equippedWeapon2 = PlayerPrefs.GetInt($"Minion_{ID}_Equipped2", -1);
            var equippedWeapon3 = PlayerPrefs.GetInt($"Minion_{ID}_Equipped3", -1);

            Count = count;
            LevelManager.ItemLevelManager.CurrentLevel = itemLevel;
            LevelManager.ExpLevelManager.Clear();
            LevelManager.ExpLevelManager.AddExp(exp);

            _equipped[0] = WeaponManager.Instance.GetWeapon(equippedWeapon0);
            _equipped[1] = WeaponManager.Instance.GetWeapon(equippedWeapon1);
            _equipped[2] = WeaponManager.Instance.GetWeapon(equippedWeapon2);
            _equipped[3] = WeaponManager.Instance.GetWeapon(equippedWeapon3);
        }
    }
}