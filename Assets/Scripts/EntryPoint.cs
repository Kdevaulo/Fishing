using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Kdevaulo.Fishing
{
    [AddComponentMenu(nameof(EntryPoint) + " in " + nameof(Fishing))]
    internal sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private PlayerInput _playerInput;

        [SerializeField] private CrossView _crossView;
        [SerializeField] private CrossSettings _crossSettings;

        private List<IUpdatable> _updatables;
        private List<IInitializable> _initializables;

        private void Awake()
        {
            _updatables = new List<IUpdatable>();
            _initializables = new List<IInitializable>();

            var functionsProvider = new FunctionsProvider(_mainCamera, _playerInput);

            var crossController = new CrossController(_crossView, _crossSettings, functionsProvider);

            _updatables.Add(crossController);
            _initializables.Add(crossController);
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