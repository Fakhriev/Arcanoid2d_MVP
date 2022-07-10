using UnityEngine;
using Model;
using Presenter;

namespace Setuper
{
    public class MainInitializer : MonoBehaviour
    {
        [Header("Main")]
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private MainUpdater _mainUpdater;
        [SerializeField] private GameEndSetuper _gameEndSetuper;

        [Header("Player")]
        [SerializeField] private PlayerShipSetuper PlayerShipSetuper;
        [SerializeField] private PlayerFireSetuper PlayerFireSetuper;
        [SerializeField] private PlayerLaserSetuper PlayerLaserSetuper;
        [SerializeField] private PlayerDebugSetuper PlayerDebugSetuper;

        [Header("Enemy")]
        [SerializeField] private AsteroidsSetuper AsteroidSetuper;
        [SerializeField] private MiniAsteroidsSetuper MiniAsteroidsSetuper;
        [SerializeField] private UFOSetuper UFOSetuper;

        private void Start()
        {
            MinMaxPositions minMaxPositions = new MinMaxPositions(_mainCamera.ViewportToWorldPoint(Vector2.zero), _mainCamera.ViewportToWorldPoint(Vector2.one));

            PlayerShipSetuper.Init(minMaxPositions, out PlayerReferences playerReferences);
            PlayerFireSetuper.Init(minMaxPositions, playerReferences.InputSystem, playerReferences.Model);
            PlayerLaserSetuper.Init(minMaxPositions, playerReferences.InputSystem, playerReferences.Model);
            PlayerDebugSetuper.Init(playerReferences.Model, playerReferences.PlayerMovable as IVector2, out IUpdatable debugUpatable);

            MiniAsteroidsSetuper.Init(minMaxPositions);
            AsteroidSetuper.Init(minMaxPositions, MiniAsteroidsSetuper.Presenter as MiniAsteroidsPresenter);
            UFOSetuper.Init(minMaxPositions, playerReferences.View);

            IWorldObjectGenerator[] enemyGenerators = new IWorldObjectGenerator[] { AsteroidSetuper.Presenter, MiniAsteroidsSetuper.Presenter, UFOSetuper.Presenter};
            _gameEndSetuper.Init(enemyGenerators, playerReferences.Model);

            IUpdatable[] extraUpdatables = new IUpdatable[] { PlayerShipSetuper.Presenter, PlayerFireSetuper.Presenter, PlayerLaserSetuper.Presenter, 
                AsteroidSetuper.Presenter, MiniAsteroidsSetuper.Presenter, UFOSetuper.Presenter, debugUpatable };

            _mainUpdater.Init(extraUpdatables);
        }
    }
}