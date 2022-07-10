using System.Collections;
using UnityEngine;

namespace Model
{
    public class UFO : Enemy
    {
        public UFO(IMovable movementPattern, IRotatable rotationPattern, MinMaxPositions minMaxPositions) : base(movementPattern, rotationPattern, minMaxPositions)
        {

        }
    }
}