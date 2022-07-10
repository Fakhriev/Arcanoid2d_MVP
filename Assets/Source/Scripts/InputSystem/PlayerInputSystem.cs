using System;

namespace InputSystem
{
    public class PlayerInputSystem
    {
        private PlayerInput _playerInput;

        public float MoveSpeed { get; private set; }

        public float RotationSide { get; private set; }

        public bool IsBulletFiring { get; private set; }

        public Action onLaserShoot;

        public PlayerInputSystem()
        {
            _playerInput = new PlayerInput();
        }

        public void Activate()
        {
            _playerInput.Enable();

            _playerInput.Player.Move.started += context => StartMove();
            _playerInput.Player.Move.canceled += context => StopMove();

            _playerInput.Player.Rotate.started += context => StartRotation(context.ReadValue<float>());
            _playerInput.Player.Rotate.canceled += context => StopRotation();

            _playerInput.Player.Fire.started += context => StartBulletFire();
            _playerInput.Player.Fire.canceled += context => StopBulletFire();

            _playerInput.Player.Laser.performed += context => LaserShoot();
        }

        public void Deactivate()
        {
            _playerInput.Disable();

            _playerInput.Player.Move.started -= context => StartMove();
            _playerInput.Player.Move.canceled -= context => StopMove();

            _playerInput.Player.Rotate.started -= context => StartRotation(context.ReadValue<float>());
            _playerInput.Player.Rotate.canceled -= context => StopRotation();

            _playerInput.Player.Fire.started -= context => StartBulletFire();
            _playerInput.Player.Fire.canceled -= context => StopBulletFire();

            _playerInput.Player.Laser.started -= context => LaserShoot();
        }

        private void StartMove()
        {
            MoveSpeed = 1f;
        }

        private void StopMove()
        {
            MoveSpeed = 0f;
        }

        private void StartRotation(float side)
        {
            RotationSide = side;
        }

        private void StopRotation()
        {
            RotationSide = 0;
        }

        private void StartBulletFire()
        {
            IsBulletFiring = true;
        }

        private void StopBulletFire()
        {
            IsBulletFiring = false;
        }

        private void LaserShoot()
        {
            onLaserShoot?.Invoke();
        }
    }
}