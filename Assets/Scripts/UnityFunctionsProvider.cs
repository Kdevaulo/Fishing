using UnityEngine;
using UnityEngine.InputSystem;

namespace Kdevaulo.Fishing
{
    internal class UnityFunctionsProvider
    {
        private readonly Camera _mainCamera;
        private readonly PlayerInput _playerInput;

        public UnityFunctionsProvider(Camera mainCamera, PlayerInput playerInput)
        {
            _mainCamera = mainCamera;
            _playerInput = playerInput;
        }

        public Vector2 ScreenToWorldPoint(Vector2 position)
        {
            return _mainCamera.ScreenToWorldPoint(position);
        }

        public InputAction FindInputAction(string crossMoveActionName)
        {
            return _playerInput.actions.FindAction(crossMoveActionName, true);
        }
    }
}