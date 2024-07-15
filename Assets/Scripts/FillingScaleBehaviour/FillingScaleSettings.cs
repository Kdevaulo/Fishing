using UnityEngine;

namespace Kdevaulo.Fishing.FillingScaleBehaviour
{
    [CreateAssetMenu(fileName = nameof(FillingScaleSettings),
        menuName = nameof(FillingScaleBehaviour) + "/" + nameof(FillingScaleSettings))]
    public class FillingScaleSettings : ScriptableObject
    {
        [field: SerializeField] public string ScaleHoldActionName { get; private set; }
    }
}