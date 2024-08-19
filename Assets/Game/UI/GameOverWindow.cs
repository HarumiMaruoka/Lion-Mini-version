using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Lion.UI
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private float _fadeDuration = 1f;

        private CancellationTokenSource _cancellationTokenSource;

        private void OnEnable()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            FadeInAsync(_cancellationTokenSource.Token).Forget();
        }

        private void OnDisable()
        {
            _cancellationTokenSource?.Cancel();
        }

        private async UniTaskVoid FadeInAsync(CancellationToken cancellationToken)
        {
            try
            {
                _canvasGroup.alpha = 0f;
                for (float t = 0f; t < _fadeDuration; t += Time.deltaTime)
                {
                    _canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / _fadeDuration);
                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
                }
                _canvasGroup.alpha = 1f;
            }
            catch (OperationCanceledException)
            {
                // Do nothing
            }
        }
    }
}