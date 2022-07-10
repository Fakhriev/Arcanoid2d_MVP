using System;
using UnityEngine;

namespace Model
{
    public class WorldObject: ModelAbstact, IMovable, IRotatable
    {
        protected IMovable _movementPattern;
        protected IRotatable _rotationPattern;

        public Action<Vector2, WorldObject> onMove;
        public Action<Quaternion, WorldObject> onRotate;
        public Action<WorldObject> onDestroy;

        public Vector2 Position { get => _movementPattern.Position; set => _movementPattern.Position = value; }

        public Quaternion Rotation { get => _rotationPattern.Rotation; set => _rotationPattern.Rotation = value; }

        public WorldObject(IMovable movementPattern, IRotatable rotationPattern)
        {
            _movementPattern = movementPattern;
            _rotationPattern = rotationPattern;
        }

        public virtual void Move(Vector2 direction, float speed, float deltaTime)
        {
            _movementPattern.Move(direction, speed, deltaTime);

            onMove?.Invoke(Position, this);
        }

        public virtual void Rotate(float side, float rotationSpeed, float deltaTime)
        {
            _rotationPattern.Rotate(side, rotationSpeed, deltaTime);

            onRotate?.Invoke(Rotation, this);
        }

        public void Destroy()
        {
            onDestroy?.Invoke(this);
        }
    }
}