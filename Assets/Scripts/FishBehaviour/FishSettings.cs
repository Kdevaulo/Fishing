using UnityEngine;

namespace Kdevaulo.Fishing.FishBehaviour
{
    [CreateAssetMenu(fileName = nameof(FishSettings), menuName = nameof(FishBehaviour) + "/" + nameof(FishSettings))]
    internal class FishSettings : ScriptableObject
    {
        [field: SerializeField] internal FishView[] FishViewVariants { get; private set; }
        [field: SerializeField] internal int StartCount { get; private set; }
        [field: SerializeField] internal Vector2 FirstAreaCorner { get; private set; }
        [field: SerializeField] internal Vector2 LastAreaCorner { get; private set; }
    }
}