using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Kdevaulo.Fishing.FillingScaleBehaviour
{
    internal sealed class FillingScaleController : BaseController<FillingScaleView>, IInitializable, IUpdatable
    {
        private readonly FunctionsProvider _functionsProvider;
        private readonly FillingScaleSettings _scaleSettings;

        private bool _canMove;

        private float _fillingValue;
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

            View.EnableFillingPhase();
            View.SetFillingValue(0);

            _canMove = true;
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
                _canMove = false;
                _isMoving = false;
            }
        }

        private void Subscribe()
        {
            var crossPressAction = _functionsProvider.FindInputAction(_scaleSettings.ScaleHoldActionName);
            crossPressAction.performed += HandleHold;
            crossPressAction.canceled += HandleRelease;
        }
    }
}