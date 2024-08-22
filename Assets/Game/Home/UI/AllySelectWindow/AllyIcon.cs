using Lion.Ally;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lion.Home.UI
{
    public class AllyIcon : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Image _allyImage;
        [SerializeField]
        private Image _minionImage;
        [SerializeField]
        private GameObject _focus;

        private AllyData _ally;
        public AllyData Ally => _ally;

        public event Action<AllyIcon> OnClick;

        public void SetAlly(AllyData ally)
        {
            _ally = ally;
            _allyImage.sprite = ally.Icon;
            _minionImage.sprite = ally.Minion?.Icon;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
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
