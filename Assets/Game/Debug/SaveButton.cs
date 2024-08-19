using Lion.Save;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lion.LionDebugger
{
    [RequireComponent(typeof(Button))]
    public class SaveButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Save);
        }

        private void Save()
        {
            SaveManager.Instance.Save();
        }
    }
}