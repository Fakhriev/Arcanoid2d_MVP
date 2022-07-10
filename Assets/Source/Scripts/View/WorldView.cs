using UnityEngine;

namespace View
{
    public class WorldView : MonoBehaviour, IWorldViewPosition
    {
        public Vector2 Position => transform.position;

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
    }
}