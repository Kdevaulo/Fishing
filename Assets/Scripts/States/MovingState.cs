using System;
using System.Threading;

namespace Kdevaulo.Fishing.States
{
    internal class MovingState : IState
    {
        public event Action StateFinished = delegate { };

        private readonly IBehaviourController _fillingScaleController;
        private readonly IBehaviourController _crossController;

        private readonly CancellationTokenSource _cts;

        public MovingState(IBehaviourController crossController, IBehaviourController fillingScaleController)
        {
            _crossController = crossController;
            _fillingScaleController = fillingScaleController;

            _cts = new CancellationTokenSource();
        }

        async void IState.Select(CancellationToken token)
        {
            await _fillingScaleController.StartAsync(token);
            await _crossController.StartAsync(token);

            await _crossController.StopAsync(token);
            await _fillingScaleController.StopAsync(token);

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