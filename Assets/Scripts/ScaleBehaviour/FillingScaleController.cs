using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Kdevaulo.Fishing.ScaleBehaviour
{
    internal sealed class FillingScaleController : BaseController<IScaleView>, IInitializable, IUpdatable,
        IClearable, IBehaviourController
    {
        private readonly FunctionsProvider _functionsProvider;
        private readonly FillingScaleSettings _scaleSettings;

        private float _fillingValue;

        private bool _canMove;
        private bool _isMoving;

        internal FillingScaleController(IScaleView view, FillingScaleSettings scaleSettings,
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
                View.SetValue(_fillingValue);
                Debug.Log(_fillingValue);
            }
        }

        void IClearable.Clear()
        {
            Unsubscribe();
            Reset();
        }

        async UniTask IBehaviourController.StartAsync(CancellationToken token)
        {
            View.SetValue(0);
            await View.AppearAsync(token);
            _canMove = true;
        }

        async UniTask IBehaviourController.StopAsync(CancellationToken token)
        {
            Reset();
            await View.DisappearAsync(token);
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