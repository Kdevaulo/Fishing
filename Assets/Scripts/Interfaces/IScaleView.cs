using System.Threading;

using Cysharp.Threading.Tasks;

namespace Kdevaulo.Fishing
{
    public interface IScaleView
    {
        public UniTask AppearAsync(CancellationToken token);
        public UniTask DisappearAsync(CancellationToken token);
        public void SetValue(float value);
    }
}