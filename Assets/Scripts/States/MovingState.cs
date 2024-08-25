using System;
using System.Threading;

using Kdevaulo.Fishing.CrossBehaviour;
using Kdevaulo.Fishing.FillingScaleBehaviour;

namespace Kdevaulo.Fishing.States
{
    internal class MovingState : IState
    {
        public event Action StateFinished = delegate { };

        private readonly CrossController _crossController;
        private readonly FillingScaleController _scaleController;

        private readonly CancellationTokenSource _cts;

        public MovingState(CrossController crossController, FillingScaleController scaleController)
        {
            _crossController = crossController;
            _scaleController = scaleController;

            _cts = new CancellationTokenSource();
        }

        async void IState.Select()
        {
            _scaleController.HandleMovingState();
            await _crossController.HandleMovementAsync(_cts.Token);

            var targetPosition = _crossController.GetCurrentPosition();

            // todo: provide targetPosition

            StateFinished.Invoke();
        }

        void IClearable.Clear()
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }
    }
}