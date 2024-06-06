using UnityEngine;

namespace Kdevaulo.Fishing
{
    [CreateAssetMenu(fileName = nameof(CrossSettings), menuName = nameof(Fishing) + "/" + nameof(CrossSettings))]
    internal class CrossSettings : ScriptableObject
    {
        [field: SerializeField] public float MinAngle { get; private set; }
        [field: SerializeField] public float MaxAngle { get; private set; }

        [field: SerializeField] public Vector2 StartPoint { get; private set; }
        [field: SerializeField] public string CrossMoveActionName { get; private set; }
    }
}