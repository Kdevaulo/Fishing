using UnityEngine;

namespace Kdevaulo.Fishing
{
    public class TransformPositionProvider
    {
        private readonly Vector2 _startPosition;
        private readonly Vector2 _endPosition;

        public TransformPositionProvider(Transform startPositionHolder, Transform endPositionHolder)
        {
            _startPosition = startPositionHolder.localPosition;
            _endPosition = endPositionHolder.localPosition;
        }

        public Vector2 GetPosition(float value)
        {
            float t = Mathf.Clamp01(value);
            return Vector3.Lerp(_startPosition, _endPosition, t);
        }
    }
}