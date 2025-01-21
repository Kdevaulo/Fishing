using System.Collections.Generic;
using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.Fishing.CrossBehaviour;
using Kdevaulo.Fishing.FishBehaviour;
using Kdevaulo.Fishing.ScaleBehaviour;
using Kdevaulo.Fishing.States;
using Kdevaulo.Fishing.Tools;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Kdevaulo.Fishing
{
    [AddComponentMenu(nameof(EntryPoint) + " in " + nameof(Fishing))]
    internal sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerInput _playerInput;

        [Header("CrossBehaviour")]
        [SerializeField] private CrossView _crossView;
        [SerializeField] private CrossSettings _crossSettings;

        [Header("ScaleBehaviour")]
        [SerializeField] private FillingScaleView _fillingScaleView;
        [SerializeField] private FillingScaleSettings _fillingScaleSettings;

        [Header("FishBehaviour")]
        [SerializeField] private FishContainerView _fishContainerView;
        [SerializeField] private FishSettings _fishSettings;

        private readonly List<IUpdatable> _updatables = new List<IUpdatable>();
        private readonly List<IClearable> _clearables = new List<IClearable>();
        private readonly List<IInitializable> _initializables = new List<IInitializable>();

        private CancellationToken _token;

        private void Awake()
        {
            _token = gameObject.GetCancellationTokenOnDestroy();

            var functionsProvider = new FunctionsProvider(_camera, _playerInput);
            var pool = new Pool<FishView>(_fishSettings.FishViewVariants[0], 20);

            var crossPositionProvider =
                new TransformPositionProvider(_crossView.StartPositionHolder, _crossView.EndPositionHolder);

            var crossController =
                new CrossController(_crossView, _crossSettings, functionsProvider, crossPositionProvider);

            var fillingScaleController =
                new FillingScaleController(_fillingScaleView, _fillingScaleSettings, functionsProvider);

            var fishController = new FishController(_fishContainerView, _fishSettings, pool);

            var statesController =
                new StatesController(crossPositionProvider, crossController, fillingScaleController, _token);

            _updatables.Add(crossController);
            _updatables.Add(fillingScaleController);

            _initializables.Add(crossController);
            _initializables.Add(fillingScaleController);
            _initializables.Add(statesController);
            _initializables.Add(fishController);

            _clearables.Add(statesController);
        }

        private void Start()
        {
            _initializables.ForEach(x => x.Initialize());
        }

        private void Update()
        {
            _updatables.ForEach(x => x.Update());
        }
    }
}