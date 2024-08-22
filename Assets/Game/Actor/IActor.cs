using Lion.Gem;
using Lion.Gold;
using System;
using UnityEngine;

namespace Lion.Actor
{
    public interface IActor
    {
        Transform transform { get; }
        void CollectGold(int amount);
        void CollectGem(int amount);
        GameObject gameObject { get; }
        void Heal(float amount);
        void Damage(float amount);
        Status Status { get; }
    }
}