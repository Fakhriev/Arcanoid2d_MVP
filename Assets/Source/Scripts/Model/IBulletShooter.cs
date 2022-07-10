using UnityEngine;

namespace Model
{
    public delegate void DefaultDelegateSignature();

    public interface IBulletShooter
    {
        public event DefaultDelegateSignature onBulletShoot;

        public void BulletReload(float deltaTime, bool tryShoot);
    }
}