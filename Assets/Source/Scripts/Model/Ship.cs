using Model.Collisions;
using System;
using UnityEngine;

namespace Model
{
    public class Ship : WorldObjectLooped, IWorldObjectColliding, IBulletShooter, ILaserShooter, ILaserParametres
    {
        private IBulletShooter _bulletShooter;
        private ILaserShooter _laserShooter;

        public ICollidingObject CollidingObject { get; set; }

        public float LaserReloadTime => _laserShooter.LaserReloadTime;

        public int LasersCount => _laserShooter.LasersCount;


        public event DefaultDelegateSignature onBulletShoot;
        public event DefaultDelegateSignature onLaserShoot;

        public Ship(IMovable movementPattern, IRotatable rotationPattern, MinMaxPositions minMaxPositions,
            IBulletShooter bulletShooter, ILaserShooter laserShooter) : base(movementPattern, rotationPattern, minMaxPositions)
        {
            _bulletShooter = bulletShooter;
            _laserShooter = laserShooter;

            _bulletShooter.onBulletShoot += RiseBulletShootEvent;
            _laserShooter.onLaserShoot += RiseLaserShootEvent;
        }

        private void RiseBulletShootEvent()
        {
            onBulletShoot?.Invoke();
        }

        private void RiseLaserShootEvent()
        {
            onLaserShoot?.Invoke();
        }

        public void BulletReload(float deltaTime, bool tryShoot)
        {
            _bulletShooter.BulletReload(deltaTime, tryShoot);
        }

        public void LaserReload(float deltaTime)
        {
            _laserShooter.LaserReload(deltaTime);
        }

        public void TryLaserShoot()
        {
            _laserShooter.TryLaserShoot();
        }
    }
}