using Lion.Manager;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lion.Home.UI
{
    public class TitleWindow : MonoBehaviour, IPointerClickHandler
    {
        private void Start()
        {
            GameManager.Instance.GameStateController.CurrentState = GameState.MainMenu;
            gameObject.SetActive(!HomeSceneManager.Instance.IsStarted);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            HomeSceneManager.Instance.Start();
            gameObject.SetActive(false);
        }
    }
}