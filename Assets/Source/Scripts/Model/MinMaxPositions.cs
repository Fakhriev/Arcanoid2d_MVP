using UnityEngine;

namespace Model
{
    public struct MinMaxPositions
    {
        public Vector2 minPosition;
        public Vector2 maxPosition;

        public MinMaxPositions(Vector2 minPosition, Vector2 maxPosition)
        {
            this.minPosition = minPosition;
            this.maxPosition = maxPosition;
        }
    }
}