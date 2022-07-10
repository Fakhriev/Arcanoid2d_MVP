using UnityEngine;
using Model;
using Model.Collisions;
using InputSystem;
using Setuper;

namespace Presenter
{
    public class PlayerMovePresenter: PresenterAbstract
    {
        private PlayerInputSystem _playerInputSystem;
        private MinMaxPositions _minMaxPosition;

        private float _maximalVelocity;
        private float _decelerationSpeed;

        private PlayerWeaponsParametres _weaponParametres;

        public Ship Ship { get; private set; }

        public IMovable PlayerMovable { get; private set; }

        public IRotatable PlayerRotatable { get; private set; }

        public PlayerMovePresenter(WorldObjectParametres parametres, Spawner spawner, MinMaxPositions minMaxPositions, 
            PlayerInputSystem playerInputSystem, float maximalVelocity, float decelerationSpeed, PlayerWeaponsParametres weaponsParametres) : base(parametres, spawner)
        {
            _minMaxPosition = minMaxPositions;
            _playerInputSystem = playerInputSystem;

            _maximalVelocity = maximalVelocity;
            _decelerationSpeed = decelerationSpeed;

            _weaponParametres = weaponsParametres;
        }

        protected override HitsDetectionAbstract CreateHitDetection(PresenterAbstract presenter)
        {
            return new PlayerCollisions(presenter);
        }

        protected override WorldObject GenerateModel()
        {
            InertiaMovement inertiaMovement = new InertiaMovement(_parametres.startPosition, _maximalVelocity, _decelerationSpeed);
            DefaultRotation defaultRotation = new DefaultRotation(_parametres.startRotation);

            PlayerMovable = inertiaMovement;
            PlayerRotatable = defaultRotation;

            (IBulletShooter, ILaserShooter) playerWeapons = PlayerWeaponsFactory.CreatePlayerWeapons(_weaponParametres);
            Ship = new Ship(inertiaMovement, defaultRotation, _minMaxPosition, playerWeapons.Item1, playerWeapons.Item2);

            Ship.onDestroy += PlayerDead;
            return Ship;
        }

        public override void Run(float deltaTime)
        {
            foreach(WorldObject model in _modelViewDictionary.Keys)
            {
                Vector2 direction = _modelViewDictionary[model].transform.up;
                model.Move(direction, _playerInputSystem.MoveSpeed * _parametres.moveSpeed, deltaTime);
                model.Rotate(_playerInputSystem.RotationSide, _parametres.rotationSpeed, deltaTime);
            }
        }

        private void PlayerDead(WorldObject player)
        {
            _playerInputSystem.Deactivate();
        }
    }
}