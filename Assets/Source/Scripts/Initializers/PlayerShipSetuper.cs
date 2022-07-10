using UnityEngine;
using Model;
using View;
using Presenter;
using InputSystem;
using Settings;

namespace Setuper
{
    public class PlayerShipSetuper : SetuperAbstract
    {
        [SerializeField] private PlayerSettings _settings;

        private PlayerInputSystem _playerInputSystem;

        public void Init(MinMaxPositions minMaxPositions, out PlayerReferences playerReferences)
        {
            _playerInputSystem = new PlayerInputSystem();
            base.Init(minMaxPositions);

            WorldView playerView = _spawner.Get();

            PlayerMovePresenter playerMovePresenter = _presenter as PlayerMovePresenter;
            playerReferences = new PlayerReferences(_playerInputSystem, playerMovePresenter.Ship, playerMovePresenter.PlayerMovable, playerMovePresenter.PlayerRotatable, playerView);
        }

        protected override void InitSpawner()
        {
            _spawner.Init(false);
        }

        protected override void InitPresentor(MinMaxPositions minMaxPositions, Spawner spawner)
        {
            _presenter = new PlayerMovePresenter(_settings.parametres, spawner, minMaxPositions, _playerInputSystem,
                _settings.maximalVelocity, _settings.decelerationSpeed, _settings.weaponParametres);
        }

        protected override void AfterActivate()
        {
            _playerInputSystem.Activate();
        }

        protected override void Deactivate()
        {
            _playerInputSystem.Deactivate();
        }
    }
}