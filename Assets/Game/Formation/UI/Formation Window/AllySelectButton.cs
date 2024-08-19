using Lion.Ally;
using Lion.Ally.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lion.Formation.UI
{
    [RequireComponent(typeof(Button))]
    public class AllySelectButton : WeaponEquippableButton
    {
        [SerializeField] private Image _icon;
        [SerializeField] private AllyWindow _allySelectWindow;

        public override IWeaponEquippable Equippable => FormationManager.Instance.FrontlineAlly;

        public override event Action<IWeaponEquippable> OnSelected
        {
            add => FormationManager.Instance.OnAllyChanged2 += value;
            remove => FormationManager.Instance.OnAllyChanged2 -= value;
        }

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OpenWindow);
            UpdateIcon(FormationManager.Instance.FrontlineAlly);
            FormationManager.Instance.OnAllyChanged += UpdateIcon;
        }

        private void OnDestroy()
        {
            FormationManager.Instance.OnAllyChanged -= UpdateIcon;
        }

        private void OpenWindow()
        {
            _allySelectWindow.Open(mode: AllyWindow.Mode.Formation);
            _allySelectWindow.OnSelected += OnSelectedAlly;
            _allySelectWindow.OnDisabled += OnClosedWindwo;
        }

        private void OnClosedWindwo()
        {
            _allySelectWindow.OnSelected -= OnSelectedAlly;
            _allySelectWindow.OnDisabled -= OnClosedWindwo;
        }

        private void OnSelectedAlly(AllyData selected)
        {
            if (!selected.Unlocked) return;

            FormationManager.Instance.FrontlineAlly = selected;

            _allySelectWindow.Close();
        }

        private void UpdateIcon(AllyData frontlineAlly)
        {
            if (frontlineAlly)
            {
                _icon.sprite = frontlineAlly.Icon;
                _icon.color = Color.white;
            }
            else
            {
                _icon.sprite = null;
                _icon.color = Color.clear;
            }
        }
    }
}