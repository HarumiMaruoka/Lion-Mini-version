using Lion.Actor;
using System;
using UnityEngine;

namespace Lion.Weapon
{
    public class WeaponParameter : IWeaponParameter
    {
        public WeaponInstance WeaponInstance { get; }
        public WeaponStatus WeaponStatus => WeaponInstance.Status;

        public WeaponParameter(WeaponInstance weaponInstance)
        {
            WeaponInstance = weaponInstance;
        }

        public IActor Actor { get; set; }

        public float PhysicalPower => Actor.Status.PhysicalPower;
        public float MagicPower => Actor.Status.MagicPower;
        public float Range => Actor.Status.Range;
        public float Duration => 1f;
        public float AttackSpeed => Actor.Status.Speed;
        public int Amount => (int)Actor.Status.Amount;
    }
}