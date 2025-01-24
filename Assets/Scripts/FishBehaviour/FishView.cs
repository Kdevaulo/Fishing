using UnityEngine;

namespace Kdevaulo.Fishing.FishBehaviour
{
    [AddComponentMenu(nameof(FishView) + " in " + nameof(FishBehaviour))]
    internal sealed class FishView : BaseView
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Transform _transform;

        private MaterialPropertyBlock _propertyBlock;

        internal override void Initialize()
        {
            _propertyBlock = new MaterialPropertyBlock();
        }

        internal void SetColor(Color color)
        {
            _propertyBlock.SetColor("_Color", color);
            _renderer.SetPropertyBlock(_propertyBlock);
        }

        internal void SetSize(float size)
        {
            _transform.localScale = Vector3.one * size;
        }
    }
}