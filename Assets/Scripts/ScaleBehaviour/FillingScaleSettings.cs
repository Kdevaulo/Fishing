using UnityEngine;

namespace Kdevaulo.Fishing.ScaleBehaviour
{
    [CreateAssetMenu(fileName = nameof(FillingScaleSettings),
        menuName = nameof(ScaleBehaviour) + "/" + nameof(FillingScaleSettings))]
    internal class FillingScaleSettings : ScriptableObject
    {
        [field: SerializeField] internal string ScaleHoldActionName { get; private set; }
    }
}