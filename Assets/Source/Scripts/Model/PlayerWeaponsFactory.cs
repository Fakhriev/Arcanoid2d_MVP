using UnityEngine;

namespace Model
{
    public class PlayerWeaponsFactory
    {
        public static (IBulletShooter, ILaserShooter) CreatePlayerWeapons(PlayerWeaponsParametres weaponsParametres)
        {
            (IBulletShooter, ILaserShooter) pair = new();

            pair.Item1 = CreateBulletShooter(weaponsParametres.fireInterval);
            pair.Item2 = CreateLaserShooter(weaponsParametres.laserReloadTime, weaponsParametres.laserMaxCount);

            return pair;
        }

        private static IBulletShooter CreateBulletShooter(float fireInterval)
        {
            return new BulletShooter(fireInterval);
        }

        private static ILaserShooter CreateLaserShooter(float laserReloadTime, int laserMaxCount)
        {
            return new LaserShooter(laserReloadTime, laserMaxCount);
        }
    }
}