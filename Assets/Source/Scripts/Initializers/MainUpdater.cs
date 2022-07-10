using UnityEngine;
using System.Linq;
using Presenter;

namespace Setuper
{
    public class MainUpdater : MonoBehaviour
    {
        private IUpdatable[] _updatables;
        private float _deltaTime;

        public void Init(IUpdatable[] updatables)
        {
            _updatables = updatables;
        }

        private void Update()
        {
            _deltaTime = Time.deltaTime;

            for(int i = 0; i < _updatables.Length; i++)
            {
                _updatables[i].Run(_deltaTime);
            }
        }
    }
}