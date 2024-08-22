using Lion.Actor;
using Lion.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lion.Result
{
    public class ResultSceneController : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TextMeshProUGUI _playerLevelView;
        [SerializeField]
        private TMPro.TextMeshProUGUI _killCountView;
        [SerializeField]
        private Button _sceneTransitionButton;
        [SerializeField]
        private string _nextSceneName;

        private void Start()
        {
            ScreenFader.Instance.FadeOut();
            _sceneTransitionButton.onClick.AddListener(FadeIn);
            _playerLevelView.text = $"Player Level: {ResultSceneManager.Instance.PlayerLevel}";
            _killCountView.text = $"Kill Count: {ResultSceneManager.Instance.KillCount}";
        }

        private void FadeIn()
        {
            ScreenFader.Instance.FadeIn(onComplete: LoadNextScene);
        }

        private void LoadNextScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_nextSceneName);
            ExperiencePointsLevelManager.Clear();
            ResultSceneManager.Instance.Clear();
        }
    }
}