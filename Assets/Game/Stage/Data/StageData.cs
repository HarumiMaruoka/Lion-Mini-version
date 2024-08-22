using Lion.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lion.Stage
{
    public class StageData : ScriptableObject
    {
        public void Initialize()
        {
            StagePrefab = Resources.Load<GameObject>($"Stage/Stage{ID}");
            EnemySpawnData = SpawnerDataReader.Load(Resources.Load<TextAsset>($"Spawner/SpawnData_Sheet{ID}"));
        }

        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }

        public GameObject StagePrefab { get; private set; }
        public List<Enemy.Wave> EnemySpawnData { get; private set; }
    }
}