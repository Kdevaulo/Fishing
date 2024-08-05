using UnityEngine;

namespace Kdevaulo.Fishing.CrossBehaviour
{
    [AddComponentMenu(nameof(CrossView) + " in " + nameof(CrossBehaviour))]
    internal sealed class CrossView : BaseView
    {
        [SerializeField] private Transform _rotator;
        [SerializeField] private Transform _mover;

        [field: SerializeField] public Transform EndPositionHolder { get; private set; }
        [field: SerializeField] public Transform StartPositionHolder { get; private set; }

        internal void SetRotation(float angleInDegrees)
        {
            _rotator.localEulerAngles = new Vector3(0, 0, angleInDegrees);
        }

        internal void Move(Vector2 position)
        {
            _mover.localPosition = position;
        }
    }
}