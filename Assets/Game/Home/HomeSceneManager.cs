using Lion.Ally;
using Lion.Minion;
using Lion.Weapon;
using System;
using UnityEngine;

namespace Lion.Home
{
    public class HomeSceneManager
    {
        public static HomeSceneManager Instance { get; } = new HomeSceneManager();
        private HomeSceneManager() { }

        private bool _isStarted = false;
        public bool IsStarted => _isStarted;

        public void Start()
        {
            _isStarted = true;
        }

        private WeaponData _selectedPlayerWeapon;
        public WeaponData SelectedPlayerWeapon => _selectedPlayerWeapon;
        public void SelectPlayerWeapon(WeaponData weaponData)
        {
            _selectedPlayerWeapon = weaponData;
        }

        private AllyData _selectedAlly;
        public AllyData SelectedAlly => _selectedAlly;
        public void SelectAlly(AllyData allyData)
        {
            _selectedAlly = allyData;
        }

        private MinionData _selectedMinion;
        public MinionData SelectedMinion => _selectedMinion;
        public void SelectMinion(MinionData minionData)
        {
            _selectedMinion = minionData;
        }

        private string _selectedStage;
        public string SelectedStage => _selectedStage;
        public void SelectStage(string stage)
        {
            _selectedStage = stage;
        }
    }
}