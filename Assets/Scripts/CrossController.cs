using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Kdevaulo.Fishing
{
    internal sealed class CrossController : BaseController<CrossView>, IInitializable
    {
        private readonly CrossSettings _crossSettings;
        private readonly UnityFunctionsProvider _functionsProvider;

        private readonly float _maxAngle;
        private readonly float _minAngle;
        private readonly float _screenHalfVerticalHeight;

        private Vector2 _startPoint;

        public CrossController(CrossView view, CrossSettings crossSettings, UnityFunctionsProvider functionsProvider) :
            base(view)
        {
            _crossSettings = crossSettings;
            _functionsProvider = functionsProvider;

            _minAngle = _crossSettings.MinAngle;
            _maxAngle = _crossSettings.MaxAngle;

            _startPoint = _crossSettings.StartPoint;
            var screenSize = new Vector2(0, Screen.height);
            _screenHalfVerticalHeight = _functionsProvider.ScreenToWorldPoint(screenSize).y / 2;
        }

        void IInitializable.Initialize()
        {
            Subscribe();
        }

        private void HandleMovement(InputAction.CallbackContext obj)
        {
            var mousePosition = _functionsProvider.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var vectorToMouse = mousePosition - _startPoint;

            float a = _screenHalfVerticalHeight + _startPoint.magnitude;
            float a2 = a * a;
            float b2 = vectorToMouse.sqrMagnitude;
            float b = Mathf.Sqrt(b2);
            float c2 = (mousePosition - new Vector2(0, _screenHalfVerticalHeight)).sqrMagnitude;
            float cos = (a2 + b2 - c2) / (2 * a * b);

            float angleMultiplier = mousePosition.x < 0 ? 1 : -1;
            float angle = Mathf.Acos(cos) * Mathf.Rad2Deg * angleMultiplier;

            float targetAngle = Mathf.Clamp(angle, _minAngle, _maxAngle);

            View.SetRotation(targetAngle);
        }

        private void Subscribe()
        {
            _functionsProvider.FindInputAction(_crossSettings.CrossMoveActionName).performed += HandleMovement;
        }
    }
}