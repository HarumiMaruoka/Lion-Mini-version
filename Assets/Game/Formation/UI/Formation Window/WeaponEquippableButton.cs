﻿using System;
using UnityEngine;

namespace Lion.Formation.UI
{
    public abstract class WeaponEquippableButton : MonoBehaviour
    {
        public abstract IWeaponEquippable Equippable { get; }
        public abstract event Action<IWeaponEquippable> OnSelected;
    }
}