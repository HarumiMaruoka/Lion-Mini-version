using Lion.Weapon;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lion.Home.UI
{
    public class WeaponIcon : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Image _iconImage;
        [SerializeField]
        private GameObject _focus;

        private WeaponData _weapon;
        public WeaponData Weapon => _weapon;

        public event Action<WeaponIcon> OnClick;

        public void SetWeapon(WeaponData weapon)
        {
            _weapon = weapon;
            _iconImage.sprite = weapon.Icon;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }

        public void Focus()
        {
            _focus.SetActive(true);
        }

        internal void Unfocus()
        {
            _focus.SetActive(false);
        }
    }
}