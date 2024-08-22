using Lion.Enemy;
using System;
using UnityEngine;

namespace Lion
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawner _spawner;
        [SerializeField]
        private TMPro.TextMeshProUGUI _view;

        private void Update()
        {
            _view.text = TimeSpan.FromSeconds(_spawner.Elapsed).ToString(@"mm\:ss");
        }
    }
}