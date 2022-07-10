using UnityEngine;

namespace Model
{
    public class Bullet : Missile
    {
        public Bullet(IMovable movementPattern, IRotatable rotationPattern, float lifeTime) : base(movementPattern, rotationPattern, lifeTime)
        {

        }
    }
}