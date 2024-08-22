using System;
using UnityEngine;

namespace Lion.Manager
{
    public class GameManager
    {
        public static GameManager Instance { get; private set; } = new GameManager();

        public GameSpeedManager GameSpeedManager { get; private set; } = new GameSpeedManager();
        public GameStateController GameStateController { get; private set; } = new GameStateController();
    }
}