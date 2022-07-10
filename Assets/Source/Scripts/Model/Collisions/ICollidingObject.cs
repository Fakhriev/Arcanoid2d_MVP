namespace Model.Collisions
{
    public delegate void WorldObjectsDouble(WorldObject first, WorldObject second);

    public interface ICollidingObject
    {
        public void Init(WorldObject worldObject);

        public event WorldObjectsDouble onCollide;
    }
}