using Lion.Manager;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Lion.Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private float _scrollDuration = 1.0f;
        [SerializeField] private ButtonWindowConnector[] _buttonWindowConnector; // �����珇�Ԃɓo�^���邱��

        private int _currentIndex = 2;

        private bool _isScrolling = false;
        private enum Direction { Left, Right, }

        private void Start()
        {
            for (int i = 0; i < _buttonWindowConnector.Length; i++)
            {
                var index = i;
                _buttonWindowConnector[i].Button.onClick.AddListener(() =>
                {
                    TransitionWindow(index);
                });
            }
        }

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private void TransitionWindow(int index)
        {
            if (_currentIndex == index) return;
            if (_isScrolling) return; // �X�N���[�����͖����B

            var old = _currentIndex;
            _currentIndex = index;

            if (_currentIndex < old) // �E�ɃX�N���[������B
            {
                StartCoroutine(
                    Scroll(Direction.Right, _scrollDuration,
                    _buttonWindowConnector[old].Window, _buttonWindowConnector[_currentIndex].Window,
                    _cancellationTokenSource.Token));
            }
            else // ���ɃX�N���[������B
            {
                StartCoroutine(
                    Scroll(Direction.Left, _scrollDuration,
                    _buttonWindowConnector[old].Window, _buttonWindowConnector[_currentIndex].Window,
                    _cancellationTokenSource.Token));
            }
        }
        private IEnumerator Scroll(Direction to, float duration, RectTransform oldWindow, RectTransform newWindow, CancellationToken token)
        {
            _isScrolling = true;
            BeginAdjust(to, oldWindow, newWindow);
            yield return UpdateAsync(duration, oldWindow, newWindow, token);
            _isScrolling = false;

            void BeginAdjust(Direction direction, RectTransform oldWindow, RectTransform newWindow)
            {
                if (direction == Direction.Left)
                {
                    // ���p�̃A���J�[�ƃs�{�b�g�ɂ���B
                    oldWindow.SetPivotWithKeepingPosition(new Vector2(1f, 0.5f));
                    oldWindow.SetAnchorWithKeepingPosition(new Vector2(0.0f, 0.5f), new Vector2(0.0f, 0.5f));
                    // �E�Ɋ񂹂�B
                    newWindow.SetPivotWithKeepingPosition(new Vector2(0f, 0.5f));
                    newWindow.SetAnchorWithKeepingPosition(new Vector2(1.0f, 0.5f), new Vector2(1.0f, 0.5f));
                    newWindow.anchoredPosition = new Vector2(0f, 0f);
                }
                else
                {
                    // �E�p�̃A���J�[�ƃs�{�b�g�ɂ���B
                    oldWindow.SetPivotWithKeepingPosition(new Vector2(0f, 0.5f));
                    oldWindow.SetAnchorWithKeepingPosition(new Vector2(1.0f, 0.5f), new Vector2(1.0f, 0.5f));
                    // ���Ɋ񂹂�B
                    newWindow.SetPivotWithKeepingPosition(new Vector2(1f, 0.5f));
                    newWindow.SetAnchorWithKeepingPosition(new Vector2(0.0f, 0.5f), new Vector2(0.0f, 0.5f));
                    newWindow.anchoredPosition = new Vector2(0f, 0f);
                }

                // �A���J�[�ƃs�{�b�g�𒆉��ɖ߂��B
                newWindow.SetPivotWithKeepingPosition(new Vector2(0.5f, 0.5f));
                newWindow.SetAnchorWithKeepingPosition(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
            }
            IEnumerator UpdateAsync(float duration, RectTransform oldWindow, RectTransform newWindow, CancellationToken token)
            {
                var oldWindowBeginPos = oldWindow.anchoredPosition;
                var newWindowBeginPos = newWindow.anchoredPosition;

                var oldWindowEndPos = new Vector2(0f, 0f);
                var newWindowEndPos = new Vector2(0f, 0f);

                for (float t = 0f; t < duration && !token.IsCancellationRequested; t += Time.deltaTime)
                {
                    oldWindow.anchoredPosition = Vector3.Slerp(oldWindowBeginPos, oldWindowEndPos, t / duration);
                    newWindow.anchoredPosition = Vector3.Slerp(newWindowBeginPos, newWindowEndPos, t / duration);

                    yield return null;
                }

                oldWindow.anchoredPosition = oldWindowEndPos;
                newWindow.anchoredPosition = newWindowEndPos;
            }
        }
    }

    [Serializable]
    public struct ButtonWindowConnector
    {
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public RectTransform Window { get; private set; }
    }
}