namespace Model.Collisions
{
    public class LaserCollision : HitsDetectionAbstract
    {
        public LaserCollision(IWorldObjectGenerator modelGenerator) : base(modelGenerator)
        {

        }

        protected override void HitAction(WorldObject first, WorldObject second)
        {
            second.Destroy();
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