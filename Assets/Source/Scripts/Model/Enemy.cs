using Model.Collisions;

namespace Model
{
    public class Enemy : WorldObjectLooped, IWorldObjectColliding
    {
        public ICollidingObject CollidingObject { get; set; }

        public Enemy(IMovable movementPattern, IRotatable rotationPattern, MinMaxPositions minMaxPositions) : base(movementPattern, rotationPattern, minMaxPositions)
        {

        }
    }
}