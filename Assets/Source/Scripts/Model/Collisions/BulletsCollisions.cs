namespace Model.Collisions
{
    public class BulletsCollisions : HitsDetectionAbstract
    {
        public BulletsCollisions(IWorldObjectGenerator modelGenerator) : base(modelGenerator)
        {

        }

        protected override void HitAction(WorldObject first, WorldObject second)
        {
            first.Destroy();
            second.Destroy();

            if(second is Asteroid)
            {
                Asteroid asteroid = second as Asteroid;
                asteroid.RaiseSpawnMiniAsteroidsEvent();
            }
        }

        protected override bool IsHitCorrect(WorldObject first, WorldObject second)
        {
            if (second is Enemy)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}