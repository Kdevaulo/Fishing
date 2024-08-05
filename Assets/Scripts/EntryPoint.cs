using System.Collections.Generic;

using Kdevaulo.Fishing.CrossBehaviour;
using Kdevaulo.Fishing.FillingScaleBehaviour;

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

        private List<IUpdatable> _updatables;
        private List<IInitializable> _initializables;

        private void Awake()
        {
            _updatables = new List<IUpdatable>();
            _initializables = new List<IInitializable>();

            var functionsProvider = new FunctionsProvider(_mainCamera, _playerInput);
            var gameplayEventsModel =
                new GameplayEventsModel(_crossView.StartPositionHolder, _crossView.EndPositionHolder);

            var crossController =
                new CrossController(_crossView, _crossSettings, functionsProvider, gameplayEventsModel);

            var fillingScaleController =
                new FillingScaleController(_fillingScaleView, _fillingScaleSettings, functionsProvider);

            _updatables.Add(crossController);
            _updatables.Add(fillingScaleController);
            _initializables.Add(crossController);
            _initializables.Add(fillingScaleController);
        }

        private void Start()
        {
            // note: change to for loop if perfomance hit is too large
            _initializables.ForEach(x => x.Initialize());
        }

        private void Update()
        {
            _updatables.ForEach(x => x.Update());
        }
    }
}