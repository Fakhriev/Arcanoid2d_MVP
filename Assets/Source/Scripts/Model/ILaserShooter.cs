namespace Model
{
    public interface ILaserShooter : ILaserParametres
    {
        public event DefaultDelegateSignature onLaserShoot;

        public void LaserReload(float deltaTime);

        public void TryLaserShoot();
    }

    public interface ILaserParametres
    {
        public float LaserReloadTime { get; }

        public int LasersCount { get; }
    }
}