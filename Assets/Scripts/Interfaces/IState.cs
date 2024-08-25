using System;

namespace Kdevaulo.Fishing
{
    public interface IState : IClearable
    {
        public event Action StateFinished;
        public void Select();
    }
}