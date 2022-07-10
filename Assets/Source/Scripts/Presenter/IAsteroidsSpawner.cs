using System.Collections;
using UnityEngine;

namespace Presenter
{
    public interface IAsteroidsSpawner
    {
        public void SpawnAsteroids(Vector2 spawnPosition);
    }
}