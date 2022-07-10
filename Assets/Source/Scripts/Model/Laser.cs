using UnityEngine;

namespace Model
{
    public class Laser : Missile
    {
        public Laser(IMovable movementPattern, IRotatable rotationPattern, float lifeTime) : base(movementPattern, rotationPattern, lifeTime)
        {

        }
    }
}