using UnityEngine;

namespace Kdevaulo.Fishing
{
    public class TransformPositionProvider
    {
        private readonly Vector2 _startPosition;
        private readonly Vector2 _endPosition;

        private Vector2 _savedPosition;

        public TransformPositionProvider(Transform startPositionHolder, Transform endPositionHolder)
        {
            _startPosition = startPositionHolder.localPosition;
            _endPosition = endPositionHolder.localPosition;
        }

        public void SavePosition(float value)
        {
            _savedPosition = GetLocalPosition(value);
        }

        public Vector2 GetSavedPosition()
        {
            return _savedPosition;
        }

        public Vector2 GetLocalPosition(float value)
        {
            float t = Mathf.Clamp01(value);
            return Vector3.Lerp(_startPosition, _endPosition, t);
        }
    }
}