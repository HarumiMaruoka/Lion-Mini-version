using System;
using UnityEngine;

namespace Lion.Actor
{
    public struct Status
    {
        public float HP;
        public float Speed;
        public float PhysicalPower;
        public float MagicPower;
        public float Defense;
        public float Range;
        public float Amount;
        public float Luck;

        public float MoveSpeed => 3f + Speed * 0.03f;
    }
}