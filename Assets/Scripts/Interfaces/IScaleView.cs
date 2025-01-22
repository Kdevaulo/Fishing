using System.Threading;

using Cysharp.Threading.Tasks;

namespace Kdevaulo.Fishing
{
    internal interface IScaleView
    {
        internal UniTask AppearAsync(CancellationToken token);
        internal UniTask DisappearAsync(CancellationToken token);
        internal void SetValue(float value);
    }
}