using Lion.Minion;
using Lion.Minion.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lion.Formation.UI
{
    [RequireComponent(typeof(Button))]
    public class MinionSelectButton : WeaponEquippableButton
    {
        [SerializeField] private Image _icon;
        [SerializeField] private int _index;
        [SerializeField] private MinionWindow _minionSelectWindow;

        public override IWeaponEquippable Equippable => FormationManager.Instance.GetFrontlineMinion(_index);

        public override event Action<IWeaponEquippable> OnSelected
        {
            add => FormationManager.Instance.OnMinionChangeds2[_index] += value;
            remove => FormationManager.Instance.OnMinionChangeds2[_index] -= value;
        }

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OpenWindow);
            UpdateIcon(FormationManager.Instance.GetFrontlineMinion(_index));
            FormationManager.Instance.OnMinionChangeds[_index] += UpdateIcon;
        }

        private void OnDestroy()
        {
            FormationManager.Instance.OnMinionChangeds[_index] -= UpdateIcon;
        }

        private void OpenWindow()
        {
            var minion = FormationManager.Instance.GetFrontlineMinion(_index);
            _minionSelectWindow.Open(mode: MinionWindow.Mode.Formation, minion: minion);
            _minionSelectWindow.OnSelected += OnSelectedMinion;
            _minionSelectWindow.OnDisabled += OnClosedWindwo;
        }

        private void OnClosedWindwo()
        {
            _minionSelectWindow.OnSelected -= OnSelectedMinion;
            _minionSelectWindow.OnDisabled -= OnClosedWindwo;
        }

        private void OnSelectedMinion(MinionData selected)
        {
            // 未解放のミニオンが選択された場合は何もしない。
            if (!selected.Unlocked) return;
            // 既にアクティブなミニオンが選択された場合は何もしない。
            var activated = FormationManager.Instance.GetFrontlineMinion(_index);
            if (activated != selected && selected.IsActive) return;

            FormationManager.Instance.ChangeFrontlineMinion(selected, _index);
            _minionSelectWindow.Close();
        }

        private void UpdateIcon(MinionData frontlineMinion)
        {
            if (frontlineMinion)
            {
                _icon.sprite = frontlineMinion.Icon;
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