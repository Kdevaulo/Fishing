using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Kdevaulo.Fishing.FillingScaleBehaviour
{
    internal sealed class FillingScaleController : BaseController<FillingScaleView>, IInitializable, IUpdatable,
        IClearable
    {
        private readonly FunctionsProvider _functionsProvider;
        private readonly FillingScaleSettings _scaleSettings;

        private float _fillingValue;

        private bool _canMove;
        private bool _isMoving;

        internal FillingScaleController(FillingScaleView view, FillingScaleSettings scaleSettings,
            FunctionsProvider functionsProvider) : base(view)
        {
            _scaleSettings = scaleSettings;
            _functionsProvider = functionsProvider;
        }

        void IInitializable.Initialize()
        {
            Subscribe();
        }

        void IUpdatable.Update()
        {
            if (_isMoving)
            {
                _fillingValue += Time.deltaTime;
                View.SetFillingValue(_fillingValue);
                Debug.Log(_fillingValue);
            }
        }

        void IClearable.Clear()
        {
            Unsubscribe();
            Reset();
        }

        public void HandleMovingState()
        {
            View.EnableFillingPhase();
            View.SetFillingValue(0);

            _canMove = true;
        }

        private void HandleHold(InputAction.CallbackContext obj)
        {
            if (obj.interaction is HoldInteraction && _canMove)
            {
                _isMoving = true;
                Debug.Log($"_isMoving = {_isMoving}");
            }
        }

        private void HandleRelease(InputAction.CallbackContext obj)
        {
            if (obj.interaction is HoldInteraction && _canMove)
            {
                Reset();
            }
        }

        private void Subscribe()
        {
            var crossPressAction = _functionsProvider.FindInputAction(_scaleSettings.ScaleHoldActionName);
            crossPressAction.performed += HandleHold;
            crossPressAction.canceled += HandleRelease;
        }

        private void Unsubscribe()
        {
            var crossPressAction = _functionsProvider.FindInputAction(_scaleSettings.ScaleHoldActionName);
            crossPressAction.performed -= HandleHold;
            crossPressAction.canceled -= HandleRelease;
        }

        private void Reset()
        {
            _canMove = false;
            _isMoving = false;
        }
    }
}