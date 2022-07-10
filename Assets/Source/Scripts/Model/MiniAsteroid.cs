using System.Collections;
using UnityEngine;

namespace Model
{
    public class MiniAsteroid : Enemy
    {
        public MiniAsteroid(IMovable movementPattern, IRotatable rotationPattern, MinMaxPositions minMaxPositions) : base(movementPattern, rotationPattern, minMaxPositions)
        {

        }
    }
}