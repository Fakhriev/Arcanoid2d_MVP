using System.Collections.Generic;

namespace Model.Collisions
{
    public abstract class HitsDetectionAbstract
    {
        protected Dictionary<WorldObject, ICollidingObject> _modelCollisionsDictionary;

        protected IWorldObjectGenerator _modelGenerator;

        public HitsDetectionAbstract(IWorldObjectGenerator modelGenerator)
        {
            _modelCollisionsDictionary = new Dictionary<WorldObject, ICollidingObject>();
            
            _modelGenerator = modelGenerator;
            _modelGenerator.onModelGenerate += OnModelGenerate;
        }

        private void OnModelGenerate(WorldObject model)
        {
            if(model is not IWorldObjectColliding)
            {
                return;
            }

            IWorldObjectColliding worldObjectColliding = model as IWorldObjectColliding;
            ICollidingObject collidingObject = worldObjectColliding.CollidingObject;

            collidingObject.Init((dynamic)model);
            _modelCollisionsDictionary.Add(model, collidingObject);

            collidingObject.onCollide += OnHit;
            model.onDestroy += OnModelDestroy;
        }

        private void OnModelDestroy(WorldObject model)
        {
            model.onDestroy -= OnModelDestroy;
            _modelCollisionsDictionary[model].onCollide -= OnHit;
            _modelCollisionsDictionary.Remove(model);
        }

        private void OnHit(WorldObject first, WorldObject second)
        {
            if (IsHitCorrect(first, second))
            {
                HitAction(first, second);
            }
        }

        protected abstract bool IsHitCorrect(WorldObject first, WorldObject second);

        protected abstract void HitAction(WorldObject first, WorldObject second);
    }
}