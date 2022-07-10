using UnityEngine;
using Model;

namespace Settings
{
    //[CreateAssetMenu(fileName = "AsteroidsSettings", menuName = "New Asteroids Settings", order = 51)]
    public class AsteroidsSettings : ScriptableObject
    {
        [Header("Asteroids")]
        public WorldObjectParametres asteroidsParametres;

        [Header("Mini Asteroids")]
        public int miniAsteroidsSpawnAmount = 3;
        public WorldObjectParametres miniAsteroidsParametres;
    }
}