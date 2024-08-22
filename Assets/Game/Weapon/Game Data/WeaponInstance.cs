using Lion.Actor;
using Lion.Weapon.Behaviour;
using System;
using UnityEngine;

namespace Lion.Weapon
{
    public class WeaponInstance
    {
        private WeaponBehaviour _gameObject;
        public WeaponParameter Parameter { get; }

        private WeaponInstance(WeaponData data, int initialLevel)
        {
            Data = data;
            Parameter = new WeaponParameter(this);
        }

        public static WeaponInstance Create(int id, int initialLevel = 1)
        {
            if (WeaponManager.Instance.WeaponSheet.TryGetValue(id, out var data))
            {
                return new WeaponInstance(data, initialLevel);
            }
            Debug.LogError($"WeaponData not found: {id}");
            return null;
        }

        public WeaponData Data { get; }

        public bool IsActive => _gameObject != null;

        public WeaponStatus Status => default;

        public event Action<bool> OnActiveChanged;

        public void Activation(IActor owner)
        {
            _gameObject = GameObject.Instantiate(Data.Prefab, owner.transform.position, Quaternion.identity);
            _gameObject.transform.SetParent(owner.transform);
            Parameter.Actor = owner;
            _gameObject.Initialize(this);
            OnActiveChanged?.Invoke(true);
        }

        public void Deactivation()
        {
            Parameter.Actor = null;
            GameObject.Destroy(_gameObject);
            _gameObject = null;
            OnActiveChanged?.Invoke(false);
        }
    }
}