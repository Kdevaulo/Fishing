using UnityEngine;

namespace Kdevaulo.Fishing.FishBehaviour
{
    [CreateAssetMenu(fileName = nameof(FishSettings), menuName = nameof(FishBehaviour) + "/" + nameof(FishSettings))]
    public class FishSettings : ScriptableObject
    {
        [field: SerializeField] internal FishView[] FishViewVariants { get; private set; }
    }
}