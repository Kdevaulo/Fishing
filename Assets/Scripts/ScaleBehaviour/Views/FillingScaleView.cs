using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace Kdevaulo.Fishing.ScaleBehaviour
{
    [AddComponentMenu(nameof(FillingScaleView) + " in " + nameof(ScaleBehaviour))]
    internal sealed class FillingScaleView : BaseView, IScaleView
    {
        [SerializeField] private CanvasRenderer _renderer;

        [SerializeField] private GameObject _container;

        [SerializeField] private Image _fillingImage;
        [SerializeField] private Image _stageIconHolder;
        
        [SerializeField] private Sprite _stageIcon;

        async UniTask IScaleView.AppearAsync(CancellationToken token)
        {
            _stageIconHolder.sprite = _stageIcon;
            _container.SetActive(true);
            _renderer.SetAlpha(0);
            //_renderer.DOFade();
            await UniTask.CompletedTask;
        }

        async UniTask IScaleView.DisappearAsync(CancellationToken token)
        {
            //_renderer.DOFade();
            await UniTask.CompletedTask;
            _renderer.SetAlpha(0);
            _container.SetActive(false);
        }

        void IScaleView.SetValue(float value)
        {
            _fillingImage.fillAmount = Mathf.Clamp01(value);
        }
    }
}