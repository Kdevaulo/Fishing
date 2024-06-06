namespace Kdevaulo.Fishing
{
    internal class BaseController<T>
    {
        protected readonly T View;

        protected BaseController(T view)
        {
            View = view;
        }
    }
}