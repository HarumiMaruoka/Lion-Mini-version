using Lion.Ally;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lion.Home.UI
{
    public class AllySelectWindow : MonoBehaviour
    {
        [SerializeField]
        private AllyIcon _iconPrefab;
        [SerializeField]
        private Transform _iconParent;

        [SerializeField]
        private Button _applyButton;
        [SerializeField]
        private GameObject _stageSelectWindow;

        private AllyIcon _selectedIcon;

        private void Start()
        {
            var allies = AllyManager.Instance.AllySheet;
            _applyButton.onClick.AddListener(ApplySelectedAlly);

            foreach (var ally in allies)
            {
                var icon = Instantiate(_iconPrefab, _iconParent);
                icon.SetAlly(ally);
                icon.OnClick += OnAllyIconClick;
            }
        }

        private void ApplySelectedAlly()
        {
            if (_selectedIcon == null) return;

            HomeSceneManager.Instance.SelectAlly(_selectedIcon.Ally);
            _stageSelectWindow.SetActive(true);
        }

        private void OnAllyIconClick(AllyIcon icon)
        {
            _selectedIcon?.Unfocus();
            _selectedIcon = icon;
            _selectedIcon?.Focus();
        }
    }
}