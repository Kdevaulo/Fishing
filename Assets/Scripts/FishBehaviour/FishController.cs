namespace Kdevaulo.Fishing.FishBehaviour
{
    internal sealed class FishController : BaseController<FishContainerView>, IInitializable
    {
        private readonly FishSettings _settings;

        public FishController(FishContainerView view, FishSettings settings) : base(view)
        {
            _settings = settings;
        }

        void IInitializable.Initialize()
        {
        }
    }
}