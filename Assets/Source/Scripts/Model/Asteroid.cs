using System;
using UnityEngine;

namespace Model
{
    public class Asteroid : Enemy
    {
        public Action<Asteroid> onMiniAsteroidsSpawn;

        public Asteroid(IMovable movementPattern, IRotatable rotationPattern, MinMaxPositions minMaxPositions) : base(movementPattern, rotationPattern, minMaxPositions)
        {

        }

        public void RaiseSpawnMiniAsteroidsEvent()
        {
            onMiniAsteroidsSpawn?.Invoke(this);
        }
    }
}