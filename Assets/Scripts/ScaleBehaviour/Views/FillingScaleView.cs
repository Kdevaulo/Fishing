using System.Threading;

using Cysharp.Threading.Tasks;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace Kdevaulo.Fishing.ScaleBehaviour
{
    [AddComponentMenu(nameof(FillingScaleView) + " in " + nameof(ScaleBehaviour))]
    internal sealed class FillingScaleView : BaseView, IScaleView
    {
        [SerializeField] private float _fadeDuration;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _container;

        [SerializeField] private Image _fillingImage;
        [SerializeField] private Image _stageIconHolder;

        [SerializeField] private Sprite _stageIcon;

        async UniTask IScaleView.AppearAsync(CancellationToken token)
        {
            _stageIconHolder.sprite = _stageIcon;
            _container.SetActive(true);

            await _canvasGroup.DOFade(1, _fadeDuration).ToUniTask(cancellationToken: token);
        }

        async UniTask IScaleView.DisappearAsync(CancellationToken token)
        {
            await _canvasGroup.DOFade(0, _fadeDuration).ToUniTask(cancellationToken: token);
            _container.SetActive(false);
        }

        void IScaleView.SetValue(float value)
        {
            _fillingImage.fillAmount = Mathf.Clamp01(value);
        }
    }
}