using System;
using System.Linq;

using Kdevaulo.Fishing.CrossBehaviour;
using Kdevaulo.Fishing.FillingScaleBehaviour;

namespace Kdevaulo.Fishing.States
{
    internal class StatesController : IInitializable, IClearable
    {
        private readonly CrossController _crossController;
        private readonly FillingScaleController _scaleController;

        private IState[] _states;

        private int _currentStateIndex;

        public StatesController(CrossController crossController, FillingScaleController scaleController)
        {
            _crossController = crossController;
            _scaleController = scaleController;
        }

        void IInitializable.Initialize()
        {
            _states = new IState[]
            {
                new MovingState(_crossController, _scaleController),
                new BitingState(),
                new SlidingState()
            };

            foreach (var state in _states)
            {
                state.StateFinished += ChangeState;
            }

            _currentStateIndex = 0;
            _states.First().Select();
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

            _states[_currentStateIndex].Select();
        }
    }
}