using UnityEngine;
using Model;
using View.Debug;
using Presenter;

namespace Setuper
{
    public class PlayerDebugSetuper : MonoBehaviour
    {
        [SerializeField] private DebugTMPPack _debugTMPPack;

        private PlayerDebugPresentor _playerDebugPresentor;

        public void Init(Ship playerModel, IVector2 velocityVector, out IUpdatable debugUpdatable)
        {
            _playerDebugPresentor = new PlayerDebugPresentor(_debugTMPPack, playerModel, velocityVector);
            debugUpdatable = _playerDebugPresentor;
        }
    }
}