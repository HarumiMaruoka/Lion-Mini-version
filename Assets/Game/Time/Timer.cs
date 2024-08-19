using System;
using UnityEngine;

namespace Lion
{
    public class Timer : MonoBehaviour
    {
        private float _elapsed = 0f;

        public float Elapsed => _elapsed;

        [SerializeField]
        private TMPro.TextMeshProUGUI _view;

        private void Update()
        {
            _elapsed += Time.deltaTime;
            _view.text = TimeSpan.FromSeconds(_elapsed).ToString(@"mm\:ss");
        }
    }
}