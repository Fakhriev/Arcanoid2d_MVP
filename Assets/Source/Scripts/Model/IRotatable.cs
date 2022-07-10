using UnityEngine;

namespace Model
{
    public interface IRotatable
    {
        public Quaternion Rotation { get; set; }

        public void Rotate(float side, float rotationSpeed, float deltaTime);
    }

    public class NoRotation : IRotatable
    {
        public NoRotation(Quaternion rotation)
        {
            Rotation = rotation;
        }

        public Quaternion Rotation { get; set; }

        public void Rotate(float side, float rotationSpeed, float deltaTime)
        {

        }
    }

    public class DefaultRotation : IRotatable
    {
        public DefaultRotation(Quaternion startRotation)
        {
            Rotation = startRotation;
        }

        public Quaternion Rotation { get; set; }

        public void Rotate(float side, float rotationSpeed, float deltaTime)
        {
            Quaternion rotationValue = Quaternion.AngleAxis(side * rotationSpeed * deltaTime, Vector3.forward);
            Rotation *= rotationValue;
        }
    }

    public class ObserveRotatable : IRotatable
    {
        private IRotatable _observeRotatable;

        public Quaternion Rotation { get ; set ; }

        public ObserveRotatable(IRotatable observeRotatable)
        {
            _observeRotatable = observeRotatable;
            Rotation = _observeRotatable.Rotation;
        }

        public void Rotate(float side, float rotationSpeed, float deltaTime)
        {
            Rotation = _observeRotatable.Rotation;
        }
    }
}