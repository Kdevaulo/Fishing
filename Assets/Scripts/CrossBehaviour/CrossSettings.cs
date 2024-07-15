using UnityEngine;

namespace Kdevaulo.Fishing.CrossBehaviour
{
    [CreateAssetMenu(fileName = nameof(CrossSettings), menuName = nameof(CrossBehaviour) + "/" + nameof(CrossSettings))]
    internal sealed class CrossSettings : ScriptableObject
    {
        [field: SerializeField] public float MinAngle { get; private set; }
        [field: SerializeField] public float MaxAngle { get; private set; }
        [field: Range(1, 25)]
        [field: SerializeField] public float SmoothMovementDecay { get; private set; }

        [field: SerializeField] public Vector2 StartPoint { get; private set; }
        [field: SerializeField] public string CrossMoveActionName { get; private set; }
        [field: SerializeField] public string CrossPressActionName { get; private set; }
    }
}