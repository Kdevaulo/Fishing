using UnityEngine;

namespace Kdevaulo.Fishing
{
    public class TransformPositionProvider
    {
        private readonly Transform _startPositionHolder;
        private readonly Transform _endPositionHolder;

        private Vector2 _startPosition;
        private Vector2 _endPosition;

        private Vector2 _savedPosition;

        public TransformPositionProvider(Transform startPositionHolder, Transform endPositionHolder)
        {
            _startPositionHolder = startPositionHolder;
            _endPositionHolder = endPositionHolder;
        }

        public void Initialize()
        {
            _startPosition = _startPositionHolder.position;
            _endPosition = _endPositionHolder.position;
        }

        public void SavePosition(float value)
        {
            _savedPosition = GetPosition(value);
        }

        public Vector2 GetSavedPosition()
        {
            return _savedPosition;
        }

        public Vector2 GetPosition(float value)
        {
            float t = Mathf.Clamp01(value);
            return Vector3.Lerp(_startPosition, _endPosition, t);
        }
    }
}