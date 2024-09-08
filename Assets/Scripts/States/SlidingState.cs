using System;
using System.Threading;

namespace Kdevaulo.Fishing.States
{
    public class SlidingState : IState
    {
        public event Action StateFinished = delegate {  };

        void IState.Select(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        void IClearable.Clear()
        {
            throw new NotImplementedException();
        }
    }
}