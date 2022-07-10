using UnityEngine;
using Model;
using Model.Collisions;
using View;
using Setuper;

namespace Presenter
{
    public class UFOPresenter : PresenterAbstract
    {
        private IWorldViewPosition _playerView;
        private MinMaxPositions _minMaxPosition;

        public UFOPresenter(WorldObjectParametres parametres, Spawner spawner, MinMaxPositions minMaxPositions, IWorldViewPosition playerView) : base(parametres, spawner)
        {
            _playerView = playerView;
            _minMaxPosition = minMaxPositions;
        }

        protected override HitsDetectionAbstract CreateHitDetection(PresenterAbstract presenter)
        {
            return new UFOCollisions(presenter);
        }

        protected override WorldObject GenerateModel()
        {
            ChaseMovemenet chaseMovement = new ChaseMovemenet(_parametres.startPosition);
            NoRotation noRotation = new NoRotation(_parametres.startRotation);

            UFO ufoModel = new UFO(chaseMovement, noRotation, _minMaxPosition);
            return ufoModel;
        }

        public override void Run(float deltaTime)
        {
            foreach(WorldObject model in _modelViewDictionary.Keys)
            {
                model.Move(_playerView.Position, _parametres.moveSpeed, deltaTime);
                model.Rotate(0, _parametres.rotationSpeed, deltaTime);
            }
        }
    }
}