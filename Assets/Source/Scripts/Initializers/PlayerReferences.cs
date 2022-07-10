using Model;
using View;
using InputSystem;

namespace Setuper
{
    public class PlayerReferences
    {
        public PlayerInputSystem InputSystem { get; private set; }
        
        public Ship Model { get; private set; }

        public IMovable PlayerMovable { get; private set; }

        public IRotatable PlayerRotatable { get; private set; }

        public WorldView View { get; private set; }

        public PlayerReferences(PlayerInputSystem inputSystem, Ship model, IMovable playerMovable, IRotatable playerRotatable, WorldView view)
        {
            InputSystem = inputSystem;
            Model = model;
            PlayerMovable = playerMovable;
            PlayerRotatable = playerRotatable;
            View = view;
        }
    }
}