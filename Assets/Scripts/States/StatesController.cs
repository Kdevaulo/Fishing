using System;
using System.Linq;
using System.Threading;

using Kdevaulo.Fishing.CrossBehaviour;
using Kdevaulo.Fishing.ScaleBehaviour;

namespace Kdevaulo.Fishing.States
{
    internal class StatesController : IInitializable, IClearable
    {
        private readonly TransformPositionProvider _crossPositionProvider;
        private readonly FillingScaleController _fillingScaleController;
        private readonly CrossController _crossController;

        private readonly CancellationToken _token;

        private IState[] _states;

        private int _currentStateIndex;

        public StatesController(
            TransformPositionProvider crossPositionProvider,
            CrossController crossController,
            FillingScaleController fillingScaleController,
            CancellationToken token)
        {
            _crossPositionProvider = crossPositionProvider;
            _crossController = crossController;
            _fillingScaleController = fillingScaleController;
            _token = token;
        }

        void IInitializable.Initialize()
        {
            _states = new IState[]
            {
                new MovingState(_crossController, _fillingScaleController),
                new BitingState(_crossPositionProvider),
                new SlidingState()
            };

            foreach (var state in _states)
            {
                state.StateFinished += ChangeState;
            }

            _currentStateIndex = 0;
            _states.First().Select(_token);
        }

        void IClearable.Clear()
        {
            foreach (var state in _states)
            {
                state.Clear();
            }
        }

        private void ChangeState()
        {
            if (++_currentStateIndex >= _states.Length)
            {
                _currentStateIndex = 0;
            }

            _states[_currentStateIndex].Select(_token);
        }
    }
}