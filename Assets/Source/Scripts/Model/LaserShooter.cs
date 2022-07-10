using System.Collections;
using UnityEngine;

namespace Model
{
    public class LaserShooter : ILaserShooter, ILaserParametres
    {
        private float _laserReloadTime;
        private float _maxReloadingTime;

        private int _laserMaxCount;
        private int _laserCount;

        public float LaserReloadTime => _laserReloadTime;

        public int LasersCount => _laserCount;


        public event DefaultDelegateSignature onLaserShoot;

        public LaserShooter(float laserReloadTime, int laserMaxCount)
        {
            _laserReloadTime = laserReloadTime;
            _maxReloadingTime = laserReloadTime;

            _laserMaxCount = laserMaxCount;
            _laserCount = laserMaxCount;
        }

        public void LaserReload(float deltaTime)
        {
            if(_laserCount < _laserMaxCount)
            {
                _laserReloadTime -= deltaTime;

                if(_laserReloadTime < 0)
                {
                    _laserCount++;
                    _laserReloadTime = _maxReloadingTime;
                }
            }
        }

        public void TryLaserShoot()
        {
            if (_laserCount > 0)
            {
                _laserCount--;
                onLaserShoot?.Invoke();
            }
        }
    }
}