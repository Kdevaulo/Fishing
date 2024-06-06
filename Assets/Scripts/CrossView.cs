using UnityEngine;

namespace Kdevaulo.Fishing
{
    internal sealed class CrossView : BaseView
    {
        [SerializeField] private Transform _rotator;

        internal void SetRotation(float angleInDegrees)
        {
            _rotator.localEulerAngles = new Vector3(0, 0, angleInDegrees);
        }
    }
}