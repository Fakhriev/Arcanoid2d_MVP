using UnityEngine;
using Model;
using Presenter;
using Settings;

namespace Setuper
{
    public class MiniAsteroidsSetuper : SetuperAbstract
    {
        [SerializeField] private AsteroidsSettings _settings;

        protected override void InitSpawner()
        {
            _spawner.Init(false);
        }

        protected override void InitPresentor(MinMaxPositions minMaxPositions, Spawner spawner)
        {
            _presenter = new MiniAsteroidsPresenter(_settings.miniAsteroidsParametres, spawner, minMaxPositions, _settings.miniAsteroidsSpawnAmount);
        }
    }
}