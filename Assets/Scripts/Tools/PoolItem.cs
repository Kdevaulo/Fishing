using UnityEngine;

namespace Kdevaulo.Fishing.Tools
{
    internal sealed class PoolItem : MonoBehaviour
    {
        internal void Disable()
        {
            gameObject.SetActive(false);
        }

        internal void Enable()
        {
            gameObject.SetActive(true);
        }
    }
}