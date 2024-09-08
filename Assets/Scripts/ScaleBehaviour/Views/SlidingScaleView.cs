using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace Kdevaulo.Fishing.ScaleBehaviour
{
    [AddComponentMenu(nameof(SlidingScaleView) + " in " + nameof(ScaleBehaviour))]
    internal sealed class SlidingScaleView : BaseView, IScaleView
    {
        [SerializeField] private float _fadeDuration;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _container;
        [SerializeField] private Scrollbar _slidingBar;

        [SerializeField] private Image _stageIconHolder;
        [SerializeField] private Sprite _stageIcon;

        UniTask IScaleView.AppearAsync(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        UniTask IScaleView.DisappearAsync(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        void IScaleView.SetValue(float value)
        {
            _slidingBar.value = Mathf.Clamp01(value);
        }
    }
}