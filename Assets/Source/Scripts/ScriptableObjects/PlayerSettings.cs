using UnityEngine;
using Model;

namespace Settings
{
    //[CreateAssetMenu(fileName = "PlayerSettings", menuName = "New Player Settings", order = 51)]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Movement")]
        public float maximalVelocity;
        public float decelerationSpeed;
        public WorldObjectParametres parametres;

        [Header("Weapons")]
        public PlayerWeaponsParametres weaponParametres;
    }
}