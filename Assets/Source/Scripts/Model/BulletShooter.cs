using UnityEngine;

namespace Model
{
    public class BulletShooter : IBulletShooter
    {
        private float _fireInterval;
        private float _reloadTime;

        public event DefaultDelegateSignature onBulletShoot;

        public BulletShooter(float fireInterval)
        {
            _fireInterval = fireInterval;
            _reloadTime = fireInterval;
        }

        public void BulletReload(float deltaTime, bool tryShoot)
        {
            if (_reloadTime > 0)
            {
                _reloadTime -= deltaTime;

                if (_reloadTime < 0)
                {
                    _reloadTime = 0;
                }
            }

            if (tryShoot && _reloadTime == 0)
            {
                BulletShoot();
            }
        }

        private void BulletShoot()
        {
            _reloadTime = _fireInterval;
            onBulletShoot?.Invoke();
        }
    }
}