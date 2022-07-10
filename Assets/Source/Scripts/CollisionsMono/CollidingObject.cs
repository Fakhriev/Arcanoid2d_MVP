using UnityEngine;
using Model;
using Model.Collisions;

namespace Collisions
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CollidingObject : MonoBehaviour, ICollidingObject
    {
        public WorldObject Type { get; private set; }

        public event WorldObjectsDouble onCollide;

        public void Init(WorldObject type)
        {
            Type = type;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out CollidingObject other))
            {
                onCollide?.Invoke(Type, other.Type);
            }
        }
    }
}