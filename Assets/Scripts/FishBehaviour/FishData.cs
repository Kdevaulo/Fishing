using System;

using UnityEngine;

namespace Kdevaulo.Fishing.FishBehaviour
{
    [Serializable]
    internal sealed class FishData
    {
        [SerializeField] internal Color Color;
        [SerializeField] internal float Size;
    }
}