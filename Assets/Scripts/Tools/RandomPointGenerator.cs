using UnityEngine;

namespace Kdevaulo.Fishing.Tools
{
    internal sealed class RandomPointGenerator
    {
        private float _maxX;
        private float _maxY;
        private float _minX;
        private float _minY;

        /// <summary>
        /// Generator will return points between start and end point
        /// </summary>
        internal void Initialize(Vector2 startPoint, Vector2 endPoint)
        {
            SortValues(startPoint.x, endPoint.x, ref _maxX, ref _minX);
            SortValues(startPoint.y, endPoint.y, ref _maxY, ref _minY);
        }

        internal Vector2 Get()
        {
            var x = Random.Range(_minX, _maxX);
            var y = Random.Range(_minY, _maxY);

            return new Vector2(x, y);
        }

        private void SortValues(float a, float b, ref float maxContainer, ref float minContainer)
        {
            if (a > b)
            {
                maxContainer = a;
                minContainer = b;
            }
            else
            {
                maxContainer = b;
                minContainer = a;
            }
        }
    }
}