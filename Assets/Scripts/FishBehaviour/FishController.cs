using Kdevaulo.Fishing.Tools;

namespace Kdevaulo.Fishing.FishBehaviour
{
    internal sealed class FishController : BaseController<FishContainerView>, IInitializable
    {
        private readonly FishSettings _settings;
        private readonly Pool<FishView> _fishPool;

        public FishController(FishContainerView view, FishSettings settings, Pool<FishView> fishPool) : base(view)
        {
            _settings = settings;
            _fishPool = fishPool;
        }

        void IInitializable.Initialize()
        {
            for (var i = 0; i < _settings.StartCount; i++)
            {
                _fishPool.Get();
            }
        }
    }
}