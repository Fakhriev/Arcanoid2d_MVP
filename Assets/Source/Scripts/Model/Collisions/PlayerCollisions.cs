namespace Model.Collisions
{
    public class PlayerCollisions : HitsDetectionAbstract
    {
        public PlayerCollisions(IWorldObjectGenerator modelGenerator) : base(modelGenerator)
        {

        }

        protected override void HitAction(WorldObject first, WorldObject second)
        {
            first.Destroy();

            if(second is Asteroid)
            {
                second.Destroy();
            }
        }

        protected override bool IsHitCorrect(WorldObject first, WorldObject second)
        {
            if(second is Enemy)
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