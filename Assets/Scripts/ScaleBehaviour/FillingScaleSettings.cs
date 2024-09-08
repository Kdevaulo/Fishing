using UnityEngine;

namespace Kdevaulo.Fishing.ScaleBehaviour
{
    [CreateAssetMenu(fileName = nameof(FillingScaleSettings),
        menuName = nameof(ScaleBehaviour) + "/" + nameof(FillingScaleSettings))]
    public class FillingScaleSettings : ScriptableObject
    {
        [field: SerializeField] public string ScaleHoldActionName { get; private set; }
    }
}