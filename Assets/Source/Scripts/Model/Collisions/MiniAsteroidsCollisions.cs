namespace Model.Collisions
{
    public class MiniAsteroidsCollisions : HitsDetectionAbstract
    {
        public MiniAsteroidsCollisions(IWorldObjectGenerator modelGenerator) : base(modelGenerator)
        {

        }

        protected override void HitAction(WorldObject first, WorldObject second)
        {
            first.Destroy();

            if (second is Laser == false)
            {
                second.Destroy();
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