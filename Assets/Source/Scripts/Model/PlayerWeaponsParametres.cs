namespace Model
{
    [System.Serializable]
    public class PlayerWeaponsParametres
    {
        [UnityEngine.Header("Bullets")]
        public float fireInterval = 0.25f;
        public float bulletLifeTime = 10f;
        public WorldObjectParametres bulletsParametres;

        [UnityEngine.Header("Laser")]
        public float laserLifeTime = 1.5f;
        public float laserReloadTime = 5f;
        public int laserMaxCount = 3;
        public WorldObjectParametres laserParametres;
    }
}