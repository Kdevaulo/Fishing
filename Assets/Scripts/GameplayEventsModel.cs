using System;

using UnityEngine;

namespace Kdevaulo.Fishing
{
    public class GameplayEventsModel
    {
        public event Action<Vector2> NextStep = delegate { }; // todo: normal name

        private readonly Vector2 _startPosition;
        private readonly Vector2 _endPosition;

        public GameplayEventsModel(Transform startPositionHolder, Transform endPositionHolder)
        {
            _startPosition = startPositionHolder.localPosition;
            _endPosition = endPositionHolder.localPosition;
        }

        public void HandleValueChosen(float t)
        {
            var targetPosition = GetPosition(t);

            NextStep.Invoke(targetPosition);
        }

        public Vector2 GetPosition(float value)
        {
            float t = Mathf.Clamp01(value);
            return Vector3.Lerp(_startPosition, _endPosition, t);
        }
    }
}