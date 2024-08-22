using Lion.Stage;
using Lion.UI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Lion.Home.UI
{
    public class StageSelectWindow : MonoBehaviour
    {
        [SerializeField]
        private StageIcon _iconPrefab;
        [SerializeField]
        private Transform _iconParent;

        [SerializeField]
        private Button _applyButton;
        [SerializeField]
        private ScreenFader _screenFader;
        [SerializeField]
        private string _stageSceneName;

        private StageIcon _selectedIcon;

        private void Start()
        {
            var stages = StageManager.Instance.StageSheet;
            _applyButton.onClick.AddListener(ApplySelectedStage);

            foreach (var stage in stages)
            {
                var icon = Instantiate(_iconPrefab, _iconParent);
                icon.SetStage(stage);
                icon.OnClick += OnStageIconClick;
            }
        }

        private void OnStageIconClick(StageIcon icon)
        {
            _selectedIcon?.Unfocus();
            _selectedIcon = icon;
            _selectedIcon?.Focus();
        }

        private void ApplySelectedStage()
        {
            if (_selectedIcon == null) return;

            _screenFader.FadeIn(onComplete: () =>
            {
                HomeSceneManager.Instance.SelectStage(_selectedIcon.StageData.ID);
                SceneManager.LoadScene(_stageSceneName);
            });
        }
    }
}