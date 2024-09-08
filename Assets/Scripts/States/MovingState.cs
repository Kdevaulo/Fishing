using System;
using System.Threading;

using Kdevaulo.Fishing.CrossBehaviour;
using Kdevaulo.Fishing.ScaleBehaviour;

namespace Kdevaulo.Fishing.States
{
    internal class MovingState : IState
    {
        public event Action StateFinished = delegate { };

        private readonly IBehaviourController _behaviourController;

        private readonly CrossController _crossController;

        private readonly CancellationTokenSource _cts;

        public MovingState(CrossController crossController, IBehaviourController controller)
        {
            _crossController = crossController;
            _behaviourController = controller;

            _cts = new CancellationTokenSource();
        }

        async void IState.Select(CancellationToken token)
        {
            await _behaviourController.StartAsync(token);
            await _crossController.HandleMovementAsync(token);

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