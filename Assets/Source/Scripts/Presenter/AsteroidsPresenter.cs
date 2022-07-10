using System.Collections.Generic;
using UnityEngine;
using Model;
using Model.Collisions;
using Setuper;

namespace Presenter
{
    public class AsteroidsPresenter : PresenterAbstract
    {
        private Dictionary<WorldObject, Vector2>  _randomDirection;
        private Dictionary<WorldObject, float> _randomRotation;

        private MinMaxPositions _minMaxPosition;
        private IAsteroidsSpawner _miniAsteroidsSpawner;

        public AsteroidsPresenter(WorldObjectParametres parametres, Spawner spawner, MinMaxPositions minMaxPositions, IAsteroidsSpawner miniAsteroidsSpawner) : base(parametres, spawner)
        {
            _minMaxPosition = minMaxPositions;
            _miniAsteroidsSpawner = miniAsteroidsSpawner;

            _randomDirection = new Dictionary<WorldObject, Vector2>();
            _randomRotation = new Dictionary<WorldObject, float>();
        }

        protected override HitsDetectionAbstract CreateHitDetection(PresenterAbstract presenter)
        {
            return new AsteroidsCollisions(presenter);
        }

        protected override WorldObject GenerateModel()
        {
            Vector2 randomPosition = _parametres.startPosition;
            randomPosition.x = Random.value > 0.5f ? randomPosition.x : -randomPosition.x;

            DefaultMovement defaultMovement = new DefaultMovement(randomPosition);
            DefaultRotation defaultRotation = new DefaultRotation(_parametres.startRotation);

            Asteroid asteroidModel = new Asteroid(defaultMovement, defaultRotation, _minMaxPosition);
            GenerateRandomParametres(asteroidModel);

            asteroidModel.onMiniAsteroidsSpawn += SpawnMiniAsteroids;
            return asteroidModel;
        }

        public override void Run(float deltaTime)
        {
            foreach (WorldObject model in _modelViewDictionary.Keys)
            {
                Vector2 direction = _randomDirection[model];
                model.Move(direction, _parametres.moveSpeed, deltaTime);

                float side = _randomRotation[model];
                model.Rotate(side, _parametres.rotationSpeed, deltaTime);
            }
        }

        private void GenerateRandomParametres(WorldObject model)
        {
            float randomAngle = Random.Range(0, 361) * Mathf.Deg2Rad;
            Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
            _randomDirection.Add(model, randomDirection);

            float rotationSpeed = Random.Range(0.25f, 1f);
            float randomRotation = Random.value > 0.5f ? rotationSpeed : -rotationSpeed;
            _randomRotation.Add(model, randomRotation);
        }

        private void SpawnMiniAsteroids(Asteroid asteroid)
        {
            asteroid.onMiniAsteroidsSpawn -= SpawnMiniAsteroids;
            _miniAsteroidsSpawner.SpawnAsteroids(asteroid.Position);
        }
    }
}