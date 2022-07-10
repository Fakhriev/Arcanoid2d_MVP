using UnityEngine;
using Model;
using Presenter;
using InputSystem;
using Settings;

namespace Setuper
{
    public class PlayerLaserSetuper : SetuperAbstract
    {
        [SerializeField] private PlayerSettings _settings;

        private PlayerInputSystem _playerInputSystem;

        private IMovable _playerMovePattern;
        private IRotatable _playerRotationPattern;

        private ILaserShooter _laserShooter;

        public void Init(MinMaxPositions minMaxPositions, PlayerInputSystem playerInputSystem, Ship shipModel)
        {
            _playerInputSystem = playerInputSystem;

            _playerMovePattern = shipModel;
            _playerRotationPattern = shipModel;

            _laserShooter = shipModel;

            base.Init(minMaxPositions);
        }

        protected override void InitSpawner()
        {
            _spawner.Init(false);
        }

        protected override void InitPresentor(MinMaxPositions minMaxPositions, Spawner spawner)
        {
            _presenter = new PlayerLaserPresenter(_settings.weaponParametres.laserParametres, spawner, _playerInputSystem, _playerMovePattern, _playerRotationPattern, _laserShooter,
                _settings.weaponParametres.laserLifeTime);
        }
    }
}