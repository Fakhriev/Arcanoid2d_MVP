using UnityEngine;
using Model;
using Presenter;

namespace Setuper
{
    public abstract class SetuperAbstract : MonoBehaviour
    {
        [SerializeField] protected Spawner _spawner;

        protected PresenterAbstract _presenter;

        public PresenterAbstract Presenter => _presenter;

        public void Init(MinMaxPositions minMaxPositions)
        {
            InitPresentor(minMaxPositions, _spawner);

            InitSpawner();

            AfterActivate();
        }

        protected virtual void InitSpawner()
        {
            _spawner.Init();
        }

        protected abstract void InitPresentor(MinMaxPositions minMaxPositions, Spawner spawner);

        protected virtual void AfterActivate()
        {
            _presenter.AfterActivate();
        }

        protected virtual void Deactivate()
        {
            _presenter.Deactivate();
        }

        private void OnDestroy()
        {
            Deactivate();
        }
    }
}