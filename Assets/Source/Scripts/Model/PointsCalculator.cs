using UnityEngine;

namespace Model
{
    public class PointsCalculator
    {
        public int GetPoints(Enemy enemy)
        {
            if(enemy is MiniAsteroid)
            {
                return 1;
            }

            if(enemy is Asteroid)
            {
                return 2;
            }

            if(enemy is UFO)
            {
                return 3;
            }

            return 0;
        }
    }
}