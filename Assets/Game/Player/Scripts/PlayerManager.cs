using Lion.Actor;
using System;
using UnityEngine;

namespace Lion.Player
{
    public class PlayerManager
    {
        public static PlayerManager Instance { get; } = new PlayerManager();

        public Sprite Icon { get; } = Resources.Load<Sprite>("PlayerIcon");
        public ExperiencePointsLevelManager LevelManager { get; } = new ExperiencePointsLevelManager("LevelByStatus_Player");
        public HPManager HPManager { get; } = new HPManager();

        public Status Status => LevelManager.CurrentStatus;
    }
}