using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lion.Actor
{
    public class ExperiencePointsLevelManager
    {
        public ExperiencePointsLevelManager(string resourcePath)
        {
            LoadExperiencePointsTable(Resources.Load<TextAsset>(resourcePath));
        }

        private static int _currentLevel = 1;
        private static int _currentExp = 0;
        private List<int> _experiencePointsTable = new List<int>();
        private Dictionary<int, Status> _levelByStatus = new Dictionary<int, Status>();

        public int CurrentLevel => _currentLevel;
        public int CurrentExp => _currentExp;
        public int CurrentLevelExp => _experiencePointsTable[_currentLevel - 1];
        public int NextLevelExp
        {
            get
            {
                if (_currentLevel >= _experiencePointsTable.Count)
                {
                    return -1;
                }

                return _experiencePointsTable[_currentLevel];
            }
        }

        public event Action<int> OnExpChanged;
        public event Action<int> OnLevelChanged;

        public Status CurrentStatus => GetStatus(_currentLevel);

        public static void Clear()
        {
            _currentLevel = 1;
            _currentExp = 0;
        }

        public Status GetStatus(int level)
        {
            if (_levelByStatus.TryGetValue(level, out var status))
            {
                return status;
            }
            Debug.LogError($"Level {level} is not found in the status table.");
            return default;
        }

        public void AddExp(int exp)
        {
            _currentExp += exp;

            if (NextLevelExp == -1)
            {
                OnExpChanged?.Invoke(_currentExp);
                return;
            }

            while (_currentExp >= NextLevelExp)
            {
                _currentLevel++;
                OnLevelChanged?.Invoke(_currentLevel);

                if (NextLevelExp == -1) break;
            }
            OnExpChanged?.Invoke(_currentExp);
        }

        private void LoadExperiencePointsTable(TextAsset input)
        {
            var csv = input.LoadCsv(1);
            foreach (var row in csv)
            {
                _experiencePointsTable.Add((int)float.Parse(row[1]));
                var status = new Status
                {
                    HP = float.Parse(row[2]),
                    Speed = float.Parse(row[3]),
                    PhysicalPower = float.Parse(row[4]),
                    MagicPower = float.Parse(row[5]),
                    Defense = float.Parse(row[6]),
                    Range = float.Parse(row[7]),
                    Amount = float.Parse(row[8]),
                    Luck = float.Parse(row[9]),
                };
                _levelByStatus.Add(_experiencePointsTable.Count, status);
            }
        }
    }
}