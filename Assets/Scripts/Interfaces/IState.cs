using System;
using System.Threading;

namespace Kdevaulo.Fishing
{
    internal interface IState : IClearable
    {
        public event Action StateFinished;
        internal void Select(CancellationToken token);
    }
}