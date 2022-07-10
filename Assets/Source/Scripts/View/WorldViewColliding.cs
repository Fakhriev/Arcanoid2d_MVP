using Collisions;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(CollidingObject))]
    public class WorldViewColliding : WorldView
    {
        [SerializeField] private CollidingObject _collidingObject;

        public CollidingObject CollidingObject => _collidingObject;
    }
}