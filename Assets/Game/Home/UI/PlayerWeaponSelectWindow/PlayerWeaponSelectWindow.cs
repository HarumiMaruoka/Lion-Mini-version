using Lion.Weapon;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lion.Home.UI
{
    public class PlayerWeaponSelectWindow : MonoBehaviour
    {
        [SerializeField]
        private WeaponIcon _iconPrefab;
        [SerializeField]
        private Transform _iconParent;

        [SerializeField]
        private Button _applyButton;
        [SerializeField]
        private GameObject _allySelectWindow;

        private WeaponIcon _selectedIcon;

        private void Start()
        {
            var weapons = WeaponManager.Instance.WeaponSheet;
            _applyButton.onClick.AddListener(ApplySelectedWeapon);

            foreach (var weapon in weapons)
            {
                var icon = Instantiate(_iconPrefab, _iconParent);
                icon.SetWeapon(weapon);
                icon.OnClick += OnWeaponIconClick;
            }
        }

        private void OnWeaponIconClick(WeaponIcon icon)
        {
            _selectedIcon?.Unfocus();
            _selectedIcon = icon;
            _selectedIcon?.Focus();
        }

        private void ApplySelectedWeapon()
        {
            if (_selectedIcon == null) return;

            HomeSceneManager.Instance.SelectPlayerWeapon(_selectedIcon.Weapon);
            _allySelectWindow.SetActive(true);
        }
    }
}