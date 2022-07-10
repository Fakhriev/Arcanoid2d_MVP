using UnityEngine;
using Model.Collisions;

namespace Model
{
    public abstract class Missile : WorldObject, IWorldObjectColliding
    {
        private float _lifeTime;

        public ICollidingObject CollidingObject { get; set; }

        public Missile(IMovable movementPattern, IRotatable rotationPattern, float lifeTime) : base(movementPattern, rotationPattern)
        {
            _lifeTime = lifeTime;
        }

        public void Tick(float tick)
        {
            _lifeTime -= tick;

            if(_lifeTime <= 0)
            {
                Destroy();
            }
        }
    }
}