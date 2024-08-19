using Lion.Ally;
using Lion.Minion;
using Lion.Save;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lion.Formation
{
    /// <summary>
    /// フォーメーションの管理を行うクラス。
    /// </summary>
    public class FormationManager : ISavable
    {
        public static FormationManager Instance { get; private set; } = new FormationManager();

        private AllyData _frontlineAlly;
        private MinionData[] _frontlineMinions = new MinionData[4];

        public event Action<AllyData> OnAllyChanged;
        public event Action<IWeaponEquippable> OnAllyChanged2;
        public Action<MinionData>[] OnMinionChangeds = new Action<MinionData>[4];
        public Action<IWeaponEquippable>[] OnMinionChangeds2 = new Action<IWeaponEquippable>[4];
        public Action<int, MinionData> OnMinionChanged;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            SceneManager.sceneLoaded += Instance.OnSceneLoaded;
            SaveManager.Instance.Register(Instance);
        }

        public float BattlePower
        {
            get
            {
                float battlePower = 0f;
                if (FrontlineAlly != null) battlePower += FrontlineAlly.Status.BattlePower;
                foreach (var minion in _frontlineMinions)
                {
                    if (minion != null) battlePower += minion.Status.BattlePower;
                }
                return battlePower;
            }
        }

        public AllyData FrontlineAlly
        {
            get => _frontlineAlly;
            set
            {
                if (value != null && value.Count == 0) return;

                _frontlineAlly?.Deactivate();

                // 既に選択されているアクターが選択された場合は、選択を解除する操作とする。
                _frontlineAlly = _frontlineAlly != value ? value : null;

                _frontlineAlly?.Activate();

                ClearMinions();
                OnAllyChanged?.Invoke(_frontlineAlly);
                OnAllyChanged2?.Invoke(_frontlineAlly);
            }
        }

        public MinionData GetFrontlineMinion(int index)
        {
            if (index < 0 || index >= _frontlineMinions.Length)
            {
                Debug.LogWarning("Index is out of range.");
                return null;
            }

            return _frontlineMinions[index];
        }

        public void ChangeFrontlineMinion(MinionData next, int index)
        {
            if (index < 0 || index >= _frontlineMinions.Length)
            {
                Debug.LogWarning("Index is out of range.");
                return;
            }

            _frontlineMinions[index]?.Deactivate();

            _frontlineMinions[index] = _frontlineMinions[index] != next ? next : null;

            _frontlineMinions[index]?.Activate();

            OnMinionChangeds[index]?.Invoke(_frontlineMinions[index]);
            OnMinionChangeds2[index]?.Invoke(_frontlineMinions[index]);
            OnMinionChanged?.Invoke(index, _frontlineMinions[index]);
        }

        public void ClearMinions()
        {
            for (int i = 0; i < _frontlineMinions.Length; i++)
            {
                if (_frontlineMinions[i] == null) continue;
                _frontlineMinions[i].Deactivate();
                _frontlineMinions[i] = null;
                OnMinionChangeds[i]?.Invoke(null);
                OnMinionChangeds[i]?.Invoke(null);
                OnMinionChanged?.Invoke(i, null);
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (_frontlineAlly) _frontlineAlly.Activate();
            foreach (var minion in _frontlineMinions)
            {
                if (minion) minion.Activate();
            }
        }

        public int LoadOrder => 2;

        public void Save()
        {
            var ally = _frontlineAlly != null ? _frontlineAlly.ID : -1;

            var minion0 = _frontlineMinions[0] != null ? _frontlineMinions[0].ID : -1;
            var minion1 = _frontlineMinions[1] != null ? _frontlineMinions[1].ID : -1;
            var minion2 = _frontlineMinions[2] != null ? _frontlineMinions[2].ID : -1;
            var minion3 = _frontlineMinions[3] != null ? _frontlineMinions[3].ID : -1;

            PlayerPrefs.SetInt("FormationManager_FrontlineAlly", ally);

            PlayerPrefs.SetInt("FormationManager_FrontlineMinion0", minion0);
            PlayerPrefs.SetInt("FormationManager_FrontlineMinion1", minion1);
            PlayerPrefs.SetInt("FormationManager_FrontlineMinion2", minion2);
            PlayerPrefs.SetInt("FormationManager_FrontlineMinion3", minion3);
        }

        public void Load()
        {
            var allyID = PlayerPrefs.GetInt("FormationManager_FrontlineAlly", -1);
            FrontlineAlly = AllyManager.Instance.AllySheet.GetAllyData(allyID);

            var minion0 = PlayerPrefs.GetInt("FormationManager_FrontlineMinion0", -1);
            ChangeFrontlineMinion(MinionManager.Instance.MinionSheet.GetMinionData(minion0), 0);

            var minion1 = PlayerPrefs.GetInt("FormationManager_FrontlineMinion1", -1);
            ChangeFrontlineMinion(MinionManager.Instance.MinionSheet.GetMinionData(minion1), 1);

            var minion2 = PlayerPrefs.GetInt("FormationManager_FrontlineMinion2", -1);
            ChangeFrontlineMinion(MinionManager.Instance.MinionSheet.GetMinionData(minion2), 2);

            var minion3 = PlayerPrefs.GetInt("FormationManager_FrontlineMinion3", -1);
            ChangeFrontlineMinion(MinionManager.Instance.MinionSheet.GetMinionData(minion3), 3);
        }
    }
}