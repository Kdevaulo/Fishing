using UnityEngine;
using UnityEngine.InputSystem;

namespace Kdevaulo.Fishing
{
    internal class FunctionsProvider
    {
        private readonly Camera _mainCamera;
        private readonly PlayerInput _playerInput;

        public FunctionsProvider(Camera mainCamera, PlayerInput playerInput)
        {
            _mainCamera = mainCamera;
            _playerInput = playerInput;
        }

        public float ExpDecay(float a, float b, float decay, float deltaTimeSeconds)
        {
            return b + (a - b) * Mathf.Exp(-decay * deltaTimeSeconds);
        }

        public InputAction FindInputAction(string crossMoveActionName)
        {
            return _playerInput.actions.FindAction(crossMoveActionName, true);
        }

        public Vector2 ScreenToWorldPoint(Vector2 position)
        {
            return _mainCamera.ScreenToWorldPoint(position);
        }
    }
}