using UnityEngine;

namespace Model
{
    public class WorldObjectLooped : WorldObject
    {
        private MinMaxPositions _minMaxPositions;

        public WorldObjectLooped(IMovable movementPattern, IRotatable rotationPattern, MinMaxPositions minMaxPositions) : base(movementPattern, rotationPattern)
        {
            _minMaxPositions = minMaxPositions;
        }

        public override void Move(Vector2 direction, float speed, float deltaTime)
        {
            _movementPattern.Move(direction, speed, deltaTime);

            LoopPosition();

            onMove?.Invoke(Position, this);
        }

        private void LoopPosition()
        {
            Vector2 loopedPosition = Position;

            if (Position.x > _minMaxPositions.maxPosition.x)
                loopedPosition.x = _minMaxPositions.minPosition.x;

            if (Position.x < _minMaxPositions.minPosition.x)
                loopedPosition.x = _minMaxPositions.maxPosition.x;

            if (Position.y > _minMaxPositions.maxPosition.y)
                loopedPosition.y = _minMaxPositions.minPosition.y;

            if (Position.y < _minMaxPositions.minPosition.y)
                loopedPosition.y = _minMaxPositions.maxPosition.y;

            Position = loopedPosition;
        }
    }
}