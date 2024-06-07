using UnityEngine;
using UnityEngine.InputSystem;

namespace Kdevaulo.Fishing
{
    internal sealed class CrossController : BaseController<CrossView>, IInitializable, IUpdatable
    {
        private readonly CrossSettings _crossSettings;
        private readonly FunctionsProvider _functionsProvider;

        private readonly float _maxAngle;
        private readonly float _minAngle;
        private readonly float _screenHalfVerticalHeight;

        private Vector2 _startPoint;

        private float _targetAngle;
        private float _currentAngle;

        public CrossController(CrossView view, CrossSettings crossSettings, FunctionsProvider functionsProvider) :
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

        void IUpdatable.Update()
        {
            _currentAngle = GetSmoothedAngle(_targetAngle);

            View.SetRotation(_currentAngle);
        }

        private float CalculateAngle(Vector2 mousePosition)
        {
            var vectorToMouse = mousePosition - _startPoint;

            float a = _screenHalfVerticalHeight + _startPoint.magnitude;
            float a2 = a * a;
            float b2 = vectorToMouse.sqrMagnitude;
            float b = Mathf.Sqrt(b2);
            float c2 = (mousePosition - new Vector2(0, _screenHalfVerticalHeight)).sqrMagnitude;
            float cos = (a2 + b2 - c2) / (2 * a * b);

            float angleMultiplier = mousePosition.x < 0 ? 1 : -1;
            float angle = Mathf.Acos(cos) * Mathf.Rad2Deg * angleMultiplier;

            return Mathf.Clamp(angle, _minAngle, _maxAngle);
        }

        private float GetSmoothedAngle(float angle)
        {
            return _functionsProvider.ExpDecay(_currentAngle, angle, 10, Time.deltaTime);
        }

        private void HandleMovement(InputAction.CallbackContext obj)
        {
            var mousePosition = _functionsProvider.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            _targetAngle = CalculateAngle(mousePosition);
        }

        private void Subscribe()
        {
            _functionsProvider.FindInputAction(_crossSettings.CrossMoveActionName).performed += HandleMovement;
        }
    }
}