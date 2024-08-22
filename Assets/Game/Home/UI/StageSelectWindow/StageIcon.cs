using Lion.Stage;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lion.Home.UI
{
    public class StageIcon : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Image _iconImage = null;
        [SerializeField]
        private GameObject _focus = null;
        [SerializeField]
        private TMPro.TextMeshProUGUI _stageName = null;

        private StageData _stageData = null;
        public StageData StageData => _stageData;

        public event Action<StageIcon> OnClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }

        public void SetStage(StageData stageData)
        {
            _stageData = stageData;
            _iconImage.sprite = stageData.Icon;
            _stageName.text = stageData.Name;
        }

        public void Focus()
        {
            _focus.SetActive(true);
        }

        public void Unfocus()
        {
            _focus.SetActive(false);
        }
    }
}