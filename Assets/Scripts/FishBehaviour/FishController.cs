using Kdevaulo.Fishing.Tools;

using UnityEditor;

using UnityEngine;

namespace Kdevaulo.Fishing.FishBehaviour
{
    internal sealed class FishController : BaseController<FishContainerView>, IInitializable
    {
        private readonly FishSettings _settings;
        private readonly Pool<FishView> _fishPool;
        private readonly RandomPointGenerator _pointGenerator;

        internal FishController(FishContainerView view, FishSettings settings, Pool<FishView> fishPool,
            RandomPointGenerator pointGenerator) : base(view)
        {
            _settings = settings;
            _fishPool = fishPool;
            _pointGenerator = pointGenerator;
        }

        void IInitializable.Initialize()
        {
            _pointGenerator.Initialize(_settings.FirstAreaCorner, _settings.LastAreaCorner);

            for (var i = 0; i < _settings.StartCount; i++)
            {
                var poolItem = _fishPool.Get();
                poolItem.transform.parent = View.transform;

                var targetPosition = _pointGenerator.Get();
                poolItem.transform.localPosition = targetPosition;

                var view = _fishPool.GetView(poolItem);
                view.SetColor(new Color32(255, 162, 56, 176));
                view.SetSize(0.75f);
            }
        }
    }
}