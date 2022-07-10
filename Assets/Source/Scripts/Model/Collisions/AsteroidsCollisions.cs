namespace Model.Collisions
{
    public class AsteroidsCollisions : HitsDetectionAbstract
    {
        public AsteroidsCollisions(IWorldObjectGenerator modelGenerator) : base(modelGenerator)
        {

        }

        protected override void HitAction(WorldObject first, WorldObject second)
        {
            first.Destroy();

            if (second is Laser == false)
            {
                second.Destroy(); 

                if (second is Bullet)
                {
                    Asteroid asteroid = first as Asteroid;
                    asteroid.RaiseSpawnMiniAsteroidsEvent();
                }
            }
        }

        protected override bool IsHitCorrect(WorldObject first, WorldObject second)
        {
            if (second is Ship || second is Missile)
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