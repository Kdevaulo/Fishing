using UnityEngine;

namespace Kdevaulo.Fishing.CrossBehaviour
{
    [CreateAssetMenu(fileName = nameof(CrossSettings), menuName = nameof(CrossBehaviour) + "/" + nameof(CrossSettings))]
    internal sealed class CrossSettings : ScriptableObject
    {
        [field: SerializeField] internal float MinAngle { get; private set; }
        [field: SerializeField] internal float MaxAngle { get; private set; }
        [field: Range(1, 25)]
        [field: SerializeField] internal float SmoothMovementDecay { get; private set; }

        [field: SerializeField] internal Vector2 StartPoint { get; private set; }
        [field: SerializeField] internal string CrossMoveActionName { get; private set; }
        [field: SerializeField] internal string CrossPressActionName { get; private set; }
    }
}