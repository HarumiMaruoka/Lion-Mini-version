﻿using Lion.Ally.Skill;
using Lion.Formation;
using Lion.Player;
using Lion.Save;
using Lion.Weapon;
using System;
using UnityEngine;

namespace Lion.Ally
{
    public class AllyData : ScriptableObject, IWeaponEquippable, ISavable
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite ActorSprite { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public AllyController Prefab { get; private set; }
        [field: SerializeField] public SkillBase SkillPrefab { get; private set; }

        private AllyController _instance;
        private int _count; // 所持数
        private WeaponInstance[] _equipped = new WeaponInstance[4];

        public event Action<bool> OnActiveChanged;
        public event Action<int> OnCountChanged;
        public event Action<bool> OnUnlockStatusChanged;

        public AllyLevelManager LevelManager { get; private set; }
        public bool IsActive => _instance != null;
        public bool Unlocked => _count > 0;
        public AllyStatus Status => LevelManager.Status;
        public AllyController Instance => _instance;

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
                    OnUnlockStatusChanged?.Invoke(false);
                }

                _count = value;
                OnCountChanged?.Invoke(value);
            }
        }

        public void Initialize()
        {
            Count = 0;
            LevelManager = new AllyLevelManager(this);
            SaveManager.Instance.Register(this);
        }

        public void Activate()
        {
            _instance = Instantiate(Prefab, PlayerController.Instance.transform.position, Quaternion.identity);
            _instance.AllyData = this;
            for (int i = 0; i < _equipped.Length; i++)
            {
                _equipped[i]?.Activation(_instance);
            }
            OnActiveChanged?.Invoke(true);
        }

        public void Deactivate()
        {
            Destroy(_instance.gameObject);
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
            // カウント、装備状態、レベルを保存
            var isActive = IsActive;
            var count = Count;
            var itemLevel = LevelManager.ItemLevelManager.CurrentLevel;
            var exp = LevelManager.ExpLevelManager.CurrentExp;

            var equippedWeapon0 = WeaponManager.Instance.Inventory.IndexOf(_equipped[0]);
            var equippedWeapon1 = WeaponManager.Instance.Inventory.IndexOf(_equipped[1]);
            var equippedWeapon2 = WeaponManager.Instance.Inventory.IndexOf(_equipped[2]);
            var equippedWeapon3 = WeaponManager.Instance.Inventory.IndexOf(_equipped[3]);

            PlayerPrefs.SetInt($"AllyData{ID}_IsActive", isActive ? 1 : 0);
            PlayerPrefs.SetInt($"AllyData{ID}_Count", count);
            PlayerPrefs.SetInt($"AllyData{ID}_ItemLevel", itemLevel);
            PlayerPrefs.SetInt($"AllyData{ID}_Exp", exp);

            PlayerPrefs.SetInt($"AllyData{ID}_Equipped0", equippedWeapon0);
            PlayerPrefs.SetInt($"AllyData{ID}_Equipped1", equippedWeapon1);
            PlayerPrefs.SetInt($"AllyData{ID}_Equipped2", equippedWeapon2);
            PlayerPrefs.SetInt($"AllyData{ID}_Equipped3", equippedWeapon3);
        }

        public void Load()
        {
            // カウント、レベル、装備をロードする。
            var count = PlayerPrefs.GetInt($"AllyData{ID}_Count", 0);
            var itemLevel = PlayerPrefs.GetInt($"AllyData{ID}_ItemLevel", 1);
            var exp = PlayerPrefs.GetInt($"AllyData{ID}_Exp", 0);

            var equippedWeapon0 = PlayerPrefs.GetInt($"AllyData{ID}_Equipped0", -1);
            var equippedWeapon1 = PlayerPrefs.GetInt($"AllyData{ID}_Equipped1", -1);
            var equippedWeapon2 = PlayerPrefs.GetInt($"AllyData{ID}_Equipped2", -1);
            var equippedWeapon3 = PlayerPrefs.GetInt($"AllyData{ID}_Equipped3", -1);

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