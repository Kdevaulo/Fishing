using System;

using UnityEngine;

namespace Kdevaulo.Fishing.CrossBehaviour
{
    [AddComponentMenu(nameof(CrossView) + " in " + nameof(CrossBehaviour))]
    internal sealed class CrossView : BaseView
    {
        [SerializeField] private Transform _rotator;
        [SerializeField] private Transform _mover;

        [SerializeField] private Transform _endPositionHolder;
        [SerializeField] private Transform _startPositionHolder;

        private Vector3 _startPosition;
        private Vector3 _endPosition;

        private void Awake()
        {
            _startPosition = _startPositionHolder.localPosition;
            _endPosition = _endPositionHolder.localPosition;
        }

        internal void SetRotation(float angleInDegrees)
        {
            _rotator.localEulerAngles = new Vector3(0, 0, angleInDegrees);
        }

        internal void Move(float currentValue)
        {
            float t = Mathf.Clamp01(currentValue);

            _mover.localPosition = Vector3.Lerp(_startPosition, _endPosition, t);
        }
    }
}