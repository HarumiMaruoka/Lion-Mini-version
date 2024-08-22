using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lion.Weapon
{
    public class WeaponManager
    {
        private static WeaponManager _instance = new WeaponManager();
        public static WeaponManager Instance
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
        private WeaponManager() { }

        private bool _isInitialized = false;
        private void Initialize()
        {
            WeaponSheet = Resources.Load<WeaponSheet>("WeaponSheet");
            WeaponSheet.Initialize();
            _isInitialized = true;
        }

        public WeaponSheet WeaponSheet { get; private set; }
        public WeaponInventory Inventory { get; private set; } = new WeaponInventory();

        public WeaponInstance GetWeapon(int inventoryIndex)
        {
            if (inventoryIndex < 0 || inventoryIndex >= Inventory.Weapons.Count)
            {
                return null;
            }
            return Inventory.Weapons[inventoryIndex];
        }
    }
}