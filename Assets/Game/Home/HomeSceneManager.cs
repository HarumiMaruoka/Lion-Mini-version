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
        public WeaponData SelectedPlayerWeapon => _selectedPlayerWeapon != null ? _selectedPlayerWeapon : WeaponManager.Instance.WeaponSheet.GetMinionData(1);
        public void SelectPlayerWeapon(WeaponData weaponData)
        {
            _selectedPlayerWeapon = weaponData;
        }

        private AllyData _selectedAlly;
        public AllyData SelectedAlly => _selectedAlly != null ? _selectedAlly : AllyManager.Instance.AllySheet.GetAllyData(0);
        public void SelectAlly(AllyData allyData)
        {
            _selectedAlly = allyData;
        }

        private int _selectedStageID = 0;
        public int SelectedStageID => _selectedStageID;
        public void SelectStage(int stageID)
        {
            _selectedStageID = stageID;
        }
    }
}