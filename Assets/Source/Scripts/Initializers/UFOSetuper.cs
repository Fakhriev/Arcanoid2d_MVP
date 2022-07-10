using UnityEngine;
using Model;
using View;
using Presenter;
using Settings;

namespace Setuper
{
    public class UFOSetuper : SetuperAbstract
    {
        [SerializeField] private UFOSettings _settings;

        private IWorldViewPosition _playerView;

        public void Init(MinMaxPositions minMaxPositions, IWorldViewPosition playerView)
        {
            _playerView = playerView;
            base.Init(minMaxPositions);
        }

        protected override void InitPresentor(MinMaxPositions minMaxPositions, Spawner spawner)
        {
            _presenter = new UFOPresenter(_settings.parametres, spawner, minMaxPositions, _playerView);
        }
    }
}