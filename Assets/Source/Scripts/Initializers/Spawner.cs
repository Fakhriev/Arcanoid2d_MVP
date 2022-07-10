using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using View;

namespace Setuper
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform _activeObjects;
        [SerializeField] private Transform _sleepingObjects;

        [SerializeField] private int _defaulCapacity = 10;
        [SerializeField] private int _maxSize = 10;

        [SerializeField] private WorldView _prefab;
        [SerializeField] private float _spawnTime;

        private ObjectPool<WorldView> _pool;
        private IEnumerator _spawner;

        public Action<WorldView> onSpawn;
        public Action<WorldView> onReturn;

        public void Init(bool startSpawner = true)
        {
            _pool = new ObjectPool<WorldView>(OnPoolCreate, OnTakeFromPool, OnReturnToPool, defaultCapacity: _defaulCapacity, maxSize: _maxSize);

            if (startSpawner)
            {
                _spawner = Spawn();
                StartCoroutine(_spawner);
            }
        }

        private WorldView OnPoolCreate()
        {
            WorldView view = Instantiate(_prefab, _sleepingObjects);
            return view;
        }

        private void OnTakeFromPool(WorldView worldView)
        {
            worldView.transform.SetParent(_activeObjects);
            StartCoroutine(ActivateWithDelay(worldView));
        }

        private void OnReturnToPool(WorldView worldView)
        {
            worldView.transform.SetParent(_sleepingObjects);
            worldView.gameObject.SetActive(false);
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnTime);

                Get();

                yield return null;
            }
        }

        private IEnumerator ActivateWithDelay(WorldView view)
        {
            yield return null;
            view.gameObject.SetActive(true);
        }

        public WorldView Get()
        {
            WorldView view = _pool.Get();
            onSpawn?.Invoke(view);
            return view;
        }

        public void Return(WorldView view)
        {
            _pool.Release(view);
            onReturn?.Invoke(view);
        }

        public void Clear()
        {
            _pool.Clear();
        }

        public void StopSpawner()
        {
            StopCoroutine(_spawner);
        }
    }
}