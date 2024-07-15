using UnityEngine;
using UnityEngine.UI;

namespace Kdevaulo.Fishing.FillingScaleBehaviour
{
    [AddComponentMenu(nameof(FillingScaleView) + " in " + nameof(FillingScaleBehaviour))]
    internal sealed class FillingScaleView : BaseView
    {
        [SerializeField] private GameObject _slidingContainer;
        [SerializeField] private GameObject _fillingContainer;
        [Space]
        [SerializeField] private Scrollbar _slidingBar;
        [SerializeField] private Image _fillingImage;

        public void EnableSlidingPhase()
        {
            _slidingContainer.SetActive(true);
            _fillingContainer.SetActive(false);
        }

        public void EnableFillingPhase()
        {
            _fillingContainer.SetActive(true);
            _slidingContainer.SetActive(false);
        }

        public void SetFillingValue(float value)
        {
            _fillingImage.fillAmount = Mathf.Clamp01(value);
        }
    }
}