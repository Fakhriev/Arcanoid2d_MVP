using System;
using System.Collections.Generic;
using UnityEngine;
using Model;
using Model.Collisions;
using InputSystem;
using Setuper;

namespace Presenter
{
    public class PlayerLaserPresenter : PresenterAbstract
    {
        private IMovable _playerMovePattern;
        private IRotatable _playerRotationPattern;
        private ILaserShooter _laserShooter;

        private PlayerInputSystem _playerInputSystem;
        private List<Laser> _lasers;

        private float _laserLifeTime;

        public PlayerLaserPresenter(WorldObjectParametres parametres, Spawner spawner, PlayerInputSystem playerInputSystem,
                            IMovable playerMovePattern, IRotatable playerRotationPattern, ILaserShooter laserShooter,
                            float laserLifeTime) : base(parametres, spawner)
        {
            _lasers = new List<Laser>();

            _playerInputSystem = playerInputSystem;
            _playerInputSystem.onLaserShoot += LaserShoot;

            _playerMovePattern = playerMovePattern;
            _playerRotationPattern = playerRotationPattern;

            _laserShooter = laserShooter;
            _laserLifeTime = laserLifeTime;
        }

        public override void AfterActivate()
        {
            base.AfterActivate();
            _laserShooter.onLaserShoot += Shoot;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _laserShooter.onLaserShoot -= Shoot;
        }

        protected override HitsDetectionAbstract CreateHitDetection(PresenterAbstract presenter)
        {
            return new LaserCollision(presenter);
        }

        protected override WorldObject GenerateModel()
        {
            ObserveMovable observeMovable = new ObserveMovable(_playerMovePattern);
            ObserveRotatable observeRotatable = new ObserveRotatable(_playerRotationPattern);

            Laser laserModel = new Laser(observeMovable, observeRotatable, _laserLifeTime);
            _lasers.Add(laserModel);

            laserModel.onDestroy += RemoveLaserFromList;
            return laserModel;
        }

        public override void Run(float deltaTime)
        {
            Move(deltaTime);
            LasersTick(deltaTime);
            LaserReload(deltaTime);
        }

        private void Move(float deltaTime)
        {
            foreach (WorldObject model in _modelViewDictionary.Keys)
            {
                Vector2 direction = _modelViewDictionary[model].transform.up;
                model.Move(direction, _parametres.moveSpeed, deltaTime);
                model.Rotate(0, _parametres.rotationSpeed, deltaTime);
            }
        }

        private void LasersTick(float deltaTime)
        {
            try
            {
                foreach (Laser laser in _lasers)
                {
                    laser.Tick(deltaTime);
                }
            }
            catch (InvalidOperationException)
            {
                //Debug.Log("List was modified in the runtime.");
            }
        }

        private void LaserReload(float deltaTime)
        {
            _laserShooter.LaserReload(deltaTime);
        }

        private void LaserShoot()
        {
            _laserShooter.TryLaserShoot();
        }

        private void Shoot()
        {
            _spawner.Get();
        }

        private void RemoveLaserFromList(WorldObject laser)
        {
            laser.onDestroy -= RemoveLaserFromList;
            _lasers.Remove(laser as Laser);
        }
    }
}