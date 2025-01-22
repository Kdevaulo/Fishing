using System.Threading;

using Cysharp.Threading.Tasks;

namespace Kdevaulo.Fishing
{
    internal interface IBehaviourController
    {
        internal UniTask StartAsync(CancellationToken token);
        internal UniTask StopAsync(CancellationToken token);
    }
}