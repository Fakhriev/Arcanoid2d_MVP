using UnityEngine;

namespace Model
{
    public interface IMovable
    {
        public Vector2 Position { get; set; }

        public void Move(Vector2 direction, float speed, float deltaTime);
    }

    public class DefaultMovement : IMovable
    {
        public Vector2 Position { get; set; }

        public DefaultMovement(Vector2 startPosition)
        {
            Position = startPosition;
        }

        public void Move(Vector2 direction, float speed, float deltaTime)
        {
            Position += direction * speed * deltaTime;
        }
    }

    public class InertiaMovement : IMovable, IVector2
    {
        public Vector2 Position { get; set; }

        public Vector2 Vector2 => _velocityVector;

        private float _maximalVelocity;
        private float _decelerationSpeed;

        private Vector2 _velocityVector;

        public InertiaMovement(Vector2 startPosition, float maximalVelocity, float decelerationSpeed)
        {
            Position = startPosition;
            _maximalVelocity = maximalVelocity;
            _decelerationSpeed = decelerationSpeed;
        }

        public void Move(Vector2 direction, float speed, float deltaTime)
        {
            if (speed >= 1f)
            {
                _velocityVector += direction * speed * deltaTime;
                _velocityVector = Vector2.ClampMagnitude(_velocityVector, _maximalVelocity);
            }

            if(speed == 0f)
            {
                _velocityVector = Vector2.MoveTowards(_velocityVector, Vector2.zero, _decelerationSpeed * deltaTime);
            }

            Position += _velocityVector * deltaTime;
        }
    }

    public class ChaseMovemenet : IMovable
    {
        public Vector2 Position { get; set; }

        public ChaseMovemenet(Vector2 startPosition)
        {
            Position = startPosition;
        }

        public void Move(Vector2 direction, float speed, float deltaTime)
        {
            Position += speed * deltaTime * (direction - Position).normalized;
        }
    }

    public class ObserveMovable : IMovable
    {
        private IMovable _observeMovable;

        public Vector2 Position { get ; set ; }

        public ObserveMovable(IMovable observeMovable)
        {
            _observeMovable = observeMovable;
            Position = _observeMovable.Position;
        }

        public void Move(Vector2 direction, float speed, float deltaTime)
        {
            Position = _observeMovable.Position;
        }
    }

    public interface IVector2
    {
        public Vector2 Vector2 { get; }
    }
}