using System;
using System.Threading;

namespace Kdevaulo.Fishing
{
    public interface IState : IClearable
    {
        public event Action StateFinished;
        public void Select(CancellationToken token);
    }
}