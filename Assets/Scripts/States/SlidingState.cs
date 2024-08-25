using System;

namespace Kdevaulo.Fishing.States
{
    public class SlidingState : IState
    {
        public event Action StateFinished = delegate {  };

        void IState.Select()
        {
            throw new NotImplementedException();
        }

        void IClearable.Clear()
        {
            throw new NotImplementedException();
        }
    }
}