using UnityEngine;

namespace Model
{
    public delegate void WorldObjectCreating(WorldObject worldObject);

    public interface IWorldObjectGenerator
    {
        public event WorldObjectCreating onModelGenerate;
    }
}