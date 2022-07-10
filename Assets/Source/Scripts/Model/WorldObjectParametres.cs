using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public struct WorldObjectParametres
    {
        public Vector2 startPosition;
        public float moveSpeed;

        public Quaternion startRotation;
        public float rotationSpeed;

        public WorldObjectParametres(Vector2 startPosition, float moveSpeed, Quaternion startRotation, float rotationSpeed)
        {
            this.startPosition = startPosition;
            this.moveSpeed = moveSpeed;

            this.startRotation = startRotation;
            this.rotationSpeed = rotationSpeed;
        }
    }
}