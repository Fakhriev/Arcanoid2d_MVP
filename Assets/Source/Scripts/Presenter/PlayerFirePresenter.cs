using System;
using System.Collections.Generic;
using UnityEngine;
using Model;
using Model.Collisions;
using InputSystem;
using Setuper;

namespace Presenter
{
    public class PlayerFirePresenter : PresenterAbstract
    {
        private IMovable _playerMovePattern;
        private IRotatable _playerRotationPattern;
        private IBulletShooter _bulletShooter;

        private PlayerInputSystem _playerInputSystem;
        private List<Bullet> _bullets;

        private float _bulletLifeTime;

        public PlayerFirePresenter(WorldObjectParametres parametres, Spawner spawner, PlayerInputSystem playerInputSystem,
                            IMovable playerMovePattern, IRotatable playerRotationPattern, IBulletShooter bulletShooter, float bulletLifeTime) : base(parametres, spawner)
        {
            _bullets = new List<Bullet>();
            _playerInputSystem = playerInputSystem;

            _playerMovePattern = playerMovePattern;
            _playerRotationPattern = playerRotationPattern;

            _bulletShooter = bulletShooter;
            _bulletLifeTime = bulletLifeTime;
        }

        public override void AfterActivate()
        {
            base.AfterActivate();
            _bulletShooter.onBulletShoot += Shoot;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _bulletShooter.onBulletShoot -= Shoot;
        }

        protected override HitsDetectionAbstract CreateHitDetection(PresenterAbstract presenter)
        {
            return new BulletsCollisions(presenter);
        }

        protected override WorldObject GenerateModel()
        {
            DefaultMovement defaulMovement = new DefaultMovement(_playerMovePattern.Position);
            NoRotation noRotation = new NoRotation(_playerRotationPattern.Rotation);

            Bullet bulletModel = new Bullet(defaulMovement, noRotation, _bulletLifeTime);
            _bullets.Add(bulletModel);

            bulletModel.onDestroy += RemoveBulletFromList;
            return bulletModel;
        }

        public override void Run(float deltaTime)
        {
            Move(deltaTime);
            BulletsTick(deltaTime);
            TryBulletShoot(deltaTime);
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

        private void BulletsTick(float deltaTime)
        {
            try
            {
                foreach (Bullet bullet in _bullets)
                {
                    bullet.Tick(deltaTime);
                }
            }
            catch (InvalidOperationException)
            {
                //Debug.Log("List was modified in the runtime.");
            }
        }

        private void TryBulletShoot(float deltaTime)
        {
            _bulletShooter.BulletReload(deltaTime, _playerInputSystem.IsBulletFiring);
        }

        private void Shoot()
        {
            _spawner.Get();
        }

        private void RemoveBulletFromList(WorldObject bullet)
        {
            bullet.onDestroy -= RemoveBulletFromList;
            _bullets.Remove(bullet as Bullet);
        }
    }
}