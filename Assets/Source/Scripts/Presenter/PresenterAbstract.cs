using System.Collections.Generic;
using UnityEngine;
using Model;
using Model.Collisions;
using View;
using Setuper;

namespace Presenter
{
    public abstract class PresenterAbstract : IUpdatable, IWorldObjectGenerator
    {
        protected Spawner _spawner;

        protected Dictionary<WorldObject, WorldView> _modelViewDictionary;

        protected WorldObjectParametres _parametres;

        public event WorldObjectCreating onModelGenerate;

        private HitsDetectionAbstract _hitDetection;

        public PresenterAbstract(WorldObjectParametres parametres, Spawner spawner)
        {
            _modelViewDictionary = new Dictionary<WorldObject, WorldView>();
            _parametres = parametres;

            _spawner = spawner;
            _spawner.onSpawn += PresentNewView;
            _hitDetection = CreateHitDetection(this);
        }

        public virtual void AfterActivate()
        {

        }

        public virtual void Deactivate()
        {
            _spawner.onSpawn -= PresentNewView;
        }

        private void PresentNewView(WorldView view)
        {
            WorldObject model = GenerateModel();
            _modelViewDictionary.Add(model, view);
            TrySetCollidingObject(model, view);

            model.onMove += OnModelMove;
            model.onRotate += OnModelRotate;
            model.onDestroy += OnModelDestroy;

            onModelGenerate?.Invoke(model);
        }

        private void TrySetCollidingObject(WorldObject model, WorldView view)
        {
            if (view is WorldViewColliding && model is IWorldObjectColliding)
            {
                IWorldObjectColliding worldObjectColliding = model as IWorldObjectColliding;
                WorldViewColliding worldViewColliding = view as WorldViewColliding;
                worldObjectColliding.CollidingObject = worldViewColliding.CollidingObject;
            }
        }

        private void OnModelMove(Vector2 position, WorldObject model)
        {
            _modelViewDictionary[model].SetPosition(position);
        }

        private void OnModelRotate(Quaternion rotation, WorldObject model)
        {
            _modelViewDictionary[model].SetRotation(rotation);
        }

        private void OnModelDestroy(WorldObject model)
        {
            model.onMove -= OnModelMove;
            model.onRotate -= OnModelRotate;
            model.onDestroy -= OnModelDestroy;

            WorldView view = _modelViewDictionary[model];
            _spawner.Return(view);

            _modelViewDictionary.Remove(model);
        }

        protected abstract WorldObject GenerateModel();

        protected abstract HitsDetectionAbstract CreateHitDetection(PresenterAbstract presenter);

        public abstract void Run(float deltaTime);
    }
}