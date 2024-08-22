using Lion.Actor;
using System;
using UnityEngine;

namespace Lion.Minion
{
    public class MinionBullet : MonoBehaviour
    {
        public MinionController Controller { get; set; }
        public Status Status => Controller.Status;
    }
}