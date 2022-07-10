using UnityEngine;
using Model;
using View.Debug;

namespace Presenter
{
    public class PlayerDebugPresentor : IUpdatable
    {
        private DebugTMPPack _debugTMPPack;

        private IMovable _playerMovable;
        private IRotatable _playerRotatable;

        private ILaserParametres _laserParametres;
        private IVector2 _velocityVector;

        public PlayerDebugPresentor(DebugTMPPack debugTMPPack, Ship playerModel, IVector2 velocityVector)
        {
            _debugTMPPack = debugTMPPack;

            _playerMovable = playerModel;
            _playerRotatable = playerModel;

            _laserParametres = playerModel;
            _velocityVector = velocityVector;
        }

        public void Run(float deltaTime)
        {
            _debugTMPPack.tmpPosition.text = $"Position: [{_playerMovable.Position.x:0.0}. {_playerMovable.Position.y:0.0}]";
            _debugTMPPack.tmpRotation.text = $"Angle: {Mathf.RoundToInt(_playerRotatable.Rotation.eulerAngles.z)}";

            _debugTMPPack.tmpVelocity.text = $"Velocity: {_velocityVector.Vector2.magnitude:0.0}";

            _debugTMPPack.tmpLasersAmount.text = $"Lasers: {_laserParametres.LasersCount}";
            _debugTMPPack.tmpLaserReload.text = $"Reload: {_laserParametres.LaserReloadTime:0.0}";
        }
    }
}