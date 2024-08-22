using Lion.Actor;
using Lion.Enemy;
using Lion.Home;
using Lion.Manager;
using Lion.Player;
using Lion.Result;
using Lion.UI;
using Lion.Weapon;
using System;
using UnityEngine;

namespace Lion.Stage
{
    [DefaultExecutionOrder(-1000)]
    public class StageController : MonoBehaviour
    {
        [SerializeField]
        private bool _isBattleScene = false;
        [SerializeField]
        private PlayerController _player;
        [SerializeField]
        private EnemySpawner _enemySpawner;

        private void Awake()
        {
            PlayerManager.Instance.HPManager.Heal(PlayerManager.Instance.Status.HP);
            StageManager.Instance.IsBattleScene = _isBattleScene;
        }

        private void Start()
        {
            ScreenFader.Instance.FadeOut();
            GameManager.Instance.GameStateController.CurrentState = GameState.InGame;

            GameManager.Instance.GameStateController.OnGameStateChanged += OnGameStateChanged;

            var playerWeapon = WeaponInstance.Create(HomeSceneManager.Instance.SelectedPlayerWeapon.ID);
            var ally = HomeSceneManager.Instance.SelectedAlly;
            var minion = ally.Minion;
            var stageID = HomeSceneManager.Instance.SelectedStageID;
            var stageData = StageManager.Instance.StageSheet.GetStageData(stageID);
            var stagePrefab = stageData.StagePrefab;
            var enemySpawnData = stageData.EnemySpawnData;

            _player.EquipWeapon(playerWeapon);
            ally.Activate();
            if (minion) minion.Activate();
            else Debug.LogError($"Minion is not found. MinionID: {ally.MinionID}");
            if (stagePrefab) Instantiate(stagePrefab);
            else Debug.LogError($"StagePrefab is not found. StageID: {stageID}");
            _enemySpawner.Initialize(enemySpawnData);
        }

        private void OnDestroy()
        {
            GameManager.Instance.GameStateController.OnGameStateChanged -= OnGameStateChanged;
            ResultSceneManager.Instance.SetPlayerLevel(PlayerManager.Instance.LevelManager.CurrentLevel);
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state == GameState.GameOver)
            {
                foreach (var actor in ActorManager.Actors)
                {
                    actor.gameObject.SetActive(false);
                }
            }
        }
    }
}