using UnityEngine;
using Model.Collisions;

namespace Model
{
    public interface IWorldObjectColliding
    {
        public ICollidingObject CollidingObject { get; set; }
    }
}