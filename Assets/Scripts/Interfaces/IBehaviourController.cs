using System.Threading;

using Cysharp.Threading.Tasks;

namespace Kdevaulo.Fishing
{
    public interface IBehaviourController
    {
        public UniTask StartAsync(CancellationToken token);
        public UniTask StopAsync(CancellationToken token);
    }
}