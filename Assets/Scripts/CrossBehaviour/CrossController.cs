using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Kdevaulo.Fishing.CrossBehaviour
{
    internal sealed class CrossController : BaseController<CrossView>, IInitializable, IUpdatable, IClearable,
        IBehaviourController
    {
        private readonly TransformPositionProvider _positionProvider;
        private readonly FunctionsProvider _functionsProvider;
        private readonly CrossSettings _crossSettings;

        private readonly float _screenHalfVerticalHeight;
        private readonly float _maxAngle;
        private readonly float _minAngle;
        private readonly float _decay;

        private Vector2 _startPoint;

        private float _currentAngle;
        private float _currentValue;
        private float _targetAngle;

        private bool _positionChosen;
        private bool _initialized;
        private bool _canMove;

        internal CrossController(CrossView view, CrossSettings crossSettings, FunctionsProvider functionsProvider,
            TransformPositionProvider positionProvider) : base(view)
        {
            _functionsProvider = functionsProvider;
            _positionProvider = positionProvider;
            _crossSettings = crossSettings;

            _minAngle = _crossSettings.MinAngle;
            _maxAngle = _crossSettings.MaxAngle;

            _decay = _crossSettings.SmoothMovementDecay;
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
            if (_canMove)
            {
                _currentValue += Time.deltaTime;

                var targetPosition = _positionProvider.GetPosition(_currentValue);

                View.Move(targetPosition);
            }
            else if (_initialized)
            {
                _currentAngle = GetSmoothedAngle(_targetAngle);

                View.SetRotation(_currentAngle);
            }
        }

        void IClearable.Clear()
        {
            Reset();
            Unsubscribe();
        }

        async UniTask IBehaviourController.StartAsync(CancellationToken token)
        {
            _initialized = true;

            await UniTask.WaitUntil(() => _positionChosen, cancellationToken: token);

            _positionProvider.SavePosition(_currentValue);
        }

        async UniTask IBehaviourController.StopAsync(CancellationToken token)
        {
            Reset();
            await UniTask.CompletedTask;
        }

        private void Subscribe()
        {
            _functionsProvider.FindInputAction(_crossSettings.CrossMoveActionName).performed += HandleMovement;

            var crossPressAction = _functionsProvider.FindInputAction(_crossSettings.CrossPressActionName);
            crossPressAction.performed += HandleHold;
            crossPressAction.canceled += HandleRelease;
        }

        private void Unsubscribe()
        {
            _functionsProvider.FindInputAction(_crossSettings.CrossMoveActionName).performed -= HandleMovement;

            var crossPressAction = _functionsProvider.FindInputAction(_crossSettings.CrossPressActionName);
            crossPressAction.performed -= HandleHold;
            crossPressAction.canceled -= HandleRelease;
        }

        private void HandleRelease(InputAction.CallbackContext obj)
        {
            if (obj.interaction is HoldInteraction && _initialized)
            {
                _initialized = false;
                _canMove = false;
                _positionChosen = true;
                Debug.Log("release");
            }
        }

        private void HandleHold(InputAction.CallbackContext obj)
        {
            if (obj.interaction is HoldInteraction && _initialized)
            {
                _positionProvider.Initialize();
                _canMove = true;
                Debug.Log("hold");
            }
        }

        private void HandleMovement(InputAction.CallbackContext obj)
        {
            var mousePosition = _functionsProvider.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            _targetAngle = CalculateAngle(mousePosition);
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
            return _functionsProvider.ExpDecay(_currentAngle, angle, _decay, Time.deltaTime);
        }

        private void Reset()
        {
            _positionChosen = false;
            _initialized = false;
            _canMove = false;
        }
    }
}