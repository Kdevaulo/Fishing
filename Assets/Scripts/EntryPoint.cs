using System.Collections.Generic;

using Kdevaulo.Fishing.CrossBehaviour;
using Kdevaulo.Fishing.FillingScaleBehaviour;
using Kdevaulo.Fishing.States;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Kdevaulo.Fishing
{
    [AddComponentMenu(nameof(EntryPoint) + " in " + nameof(Fishing))]
    internal sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private PlayerInput _playerInput;

        [Header("CrossBehaviour")]
        [SerializeField] private CrossView _crossView;
        [SerializeField] private CrossSettings _crossSettings;

        [Header("ScaleBehaviour")]
        [SerializeField] private FillingScaleView _fillingScaleView;
        [SerializeField] private FillingScaleSettings _fillingScaleSettings;

        private readonly List<IUpdatable> _updatables = new List<IUpdatable>();
        private readonly List<IClearable> _clearables = new List<IClearable>();
        private readonly List<IInitializable> _initializables = new List<IInitializable>();

        private void Awake()
        {
            var functionsProvider = new FunctionsProvider(_mainCamera, _playerInput);

            var crossPositionProvider =
                new TransformPositionProvider(_crossView.StartPositionHolder, _crossView.EndPositionHolder);

            var crossController =
                new CrossController(_crossView, _crossSettings, functionsProvider, crossPositionProvider);

            var fillingScaleController =
                new FillingScaleController(_fillingScaleView, _fillingScaleSettings, functionsProvider);

            var statesController = new StatesController(crossController, fillingScaleController);

            _updatables.Add(crossController);
            _updatables.Add(fillingScaleController);

            _initializables.Add(crossController);
            _initializables.Add(fillingScaleController);
            _initializables.Add(statesController);
            
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