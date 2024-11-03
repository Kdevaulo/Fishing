using UnityEngine;
using UnityEngine.InputSystem;

namespace Kdevaulo.Fishing
{
    internal class FunctionsProvider
    {
        private readonly Camera _camera;
        private readonly PlayerInput _playerInput;

        public FunctionsProvider(Camera camera, PlayerInput playerInput)
        {
            _camera = camera;
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
            return _camera.ScreenToWorldPoint(position);
        }
    }
}