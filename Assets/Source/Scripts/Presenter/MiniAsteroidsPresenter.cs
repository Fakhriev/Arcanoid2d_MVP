using System.Collections.Generic;
using UnityEngine;
using Model;
using Model.Collisions;
using Setuper;

namespace Presenter
{
    public class MiniAsteroidsPresenter : PresenterAbstract, IAsteroidsSpawner
    {
        private Dictionary<WorldObject, Vector2> _randomDirection;
        private Dictionary<WorldObject, float> _randomRotation;

        private MinMaxPositions _minMaxPosition;
        private int _miniAsteroidsSpawnAmount;

        private Vector2 _spawnPosition;

        public MiniAsteroidsPresenter(WorldObjectParametres parametres, Spawner spawner, MinMaxPositions minMaxPositions, int miniAsteroidsSpawnAmount) : base(parametres, spawner)
        {
            _minMaxPosition = minMaxPositions;
            _miniAsteroidsSpawnAmount = miniAsteroidsSpawnAmount;

            _randomDirection = new Dictionary<WorldObject, Vector2>();
            _randomRotation = new Dictionary<WorldObject, float>();
        }

        protected override HitsDetectionAbstract CreateHitDetection(PresenterAbstract presenter)
        {
            return new MiniAsteroidsCollisions(presenter);
        }

        protected override WorldObject GenerateModel()
        {
            DefaultMovement defaultMovement = new DefaultMovement(_spawnPosition);
            DefaultRotation defaultRotation = new DefaultRotation(_parametres.startRotation);

            MiniAsteroid miniAsteroidModel = new MiniAsteroid(defaultMovement, defaultRotation, _minMaxPosition);
            GenerateRandomParametres(miniAsteroidModel);

            return miniAsteroidModel;
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

        public void SpawnAsteroids(Vector2 spawnPosition)
        {
            _spawnPosition = spawnPosition;

            for(int i = 0; i < _miniAsteroidsSpawnAmount; i++)
            {
                _spawner.Get();
            }
        }
    }
}