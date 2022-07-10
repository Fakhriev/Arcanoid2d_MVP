using UnityEngine;
using Model;
using Presenter;
using InputSystem;
using Settings;

namespace Setuper
{
    public class PlayerFireSetuper : SetuperAbstract
    {
        [SerializeField] private PlayerSettings _settings;

        private PlayerInputSystem _playerInputSystem;

        private IMovable _playerMovePattern;
        private IRotatable _playerRotationPattern;

        private IBulletShooter _bulletShooter;

        public void Init(MinMaxPositions minMaxPositions, PlayerInputSystem playerInputSystem, Ship shipModel)
        {
            _playerInputSystem = playerInputSystem;

            _playerMovePattern = shipModel;
            _playerRotationPattern = shipModel;

            _bulletShooter = shipModel;
            base.Init(minMaxPositions);
        }

        protected override void InitSpawner()
        {
            _spawner.Init(false);
        }

        protected override void InitPresentor(MinMaxPositions minMaxPositions, Spawner spawner)
        {
            _presenter = new PlayerFirePresenter(_settings.weaponParametres.bulletsParametres, spawner, _playerInputSystem,
                _playerMovePattern, _playerRotationPattern, _bulletShooter, _settings.weaponParametres.bulletLifeTime);
        }
    }
}