using UnityEngine;

namespace Presenter
{
    public interface IUpdatable    
    {
        public void Run(float deltaTime);
    }
}