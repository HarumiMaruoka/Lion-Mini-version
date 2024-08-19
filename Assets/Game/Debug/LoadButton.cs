using Lion.Save;
using Lion.UI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Lion.LionDebugger
{
    [RequireComponent(typeof(Button))]
    public class LoadButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Load);
        }

        private void Load()
        {
            ScreenFader.Instance.FadeIn(1f, () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                SaveManager.Instance.Load();
            });
        }
    }
}