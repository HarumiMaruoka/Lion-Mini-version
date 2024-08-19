using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lion.Enemy
{
    public static class SpawnerDataReader
    {
        public static List<Wave> Load(TextAsset input)
        {
            var result = new List<Wave>();
            var csv = input.LoadCsv();

            Wave currentWave = null;

            foreach (var line in csv)
            {
                var title = line[0];

                switch (title)
                {
                    case "Time(s)":
                        currentWave = new Wave { Time = float.Parse(line[1]) };
                        result.Add(currentWave);
                        break;

                    case "Rate":
                        if (currentWave != null)
                        {
                            currentWave.MinInterval = float.Parse(line[1]);
                            currentWave.MaxInterval = float.Parse(line[2]);
                        }
                        break;

                    case "Enemy":
                        if (currentWave != null)
                        {
                            currentWave.SpawnableEnemyData.Add(new SpawnableEnemyData
                            {
                                EnemyID = int.Parse(line[1]),
                                Probability = float.Parse(line[2])
                            });
                        }
                        break;

                    default:
                        Debug.LogError($"At line: {Array.IndexOf(csv, line)}.\nInvalid title: {title}.");
                        break;
                }
            }

            return result;
        }
    }

    [Serializable]
    public class Wave
    {
        public float Time;
        public float MinInterval;
        public float MaxInterval;
        public List<SpawnableEnemyData> SpawnableEnemyData = new List<SpawnableEnemyData>();
    }

    [Serializable]
    public struct SpawnableEnemyData
    {
        public int EnemyID;
        public float Probability;
    }
}