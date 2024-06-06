using System.Collections.Generic;

using Unity.VisualScripting;

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

        private List<IInitializable> _initializables;

        private void Awake()
        {
            _initializables = new List<IInitializable>();
            var functionsProvider = new UnityFunctionsProvider(_mainCamera, _playerInput);

            var crossController = new CrossController(_crossView, _crossSettings, functionsProvider);
            _initializables.Add(crossController);
        }

        private void Start()
        {
            foreach (var initializable in _initializables)
            {
                initializable.Initialize();
            }
        }
    }
}