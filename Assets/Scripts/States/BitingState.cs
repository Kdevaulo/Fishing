using System;
using System.Threading;

using UnityEngine;

namespace Kdevaulo.Fishing.States
{
    public class BitingState : IState
    {
        public event Action StateFinished = delegate { };

        private readonly TransformPositionProvider _positionProvider;

        public BitingState(TransformPositionProvider positionProvider)
        {
            _positionProvider = positionProvider;
        }

        void IState.Select(CancellationToken token)
        {
            var targetPosition = _positionProvider.GetSavedPosition();

        }

        void IClearable.Clear()
        {
            throw new NotImplementedException();
        }
    }
}