using UnityEngine;
using Model;
using Presenter;
using Settings;

namespace Setuper
{
    public class AsteroidsSetuper : SetuperAbstract
    {
        [SerializeField] private AsteroidsSettings _settings;
        
        private IAsteroidsSpawner _miniAsteroidsSpawner;

        public void Init(MinMaxPositions minMaxPositions, IAsteroidsSpawner miniAsteroidsSpawner)
        {
            _miniAsteroidsSpawner = miniAsteroidsSpawner;
            base.Init(minMaxPositions);
        }

        protected override void InitPresentor(MinMaxPositions minMaxPositions, Spawner spawner)
        {
            _presenter = new AsteroidsPresenter(_settings.asteroidsParametres, spawner, minMaxPositions, _miniAsteroidsSpawner);
        }
    }
}