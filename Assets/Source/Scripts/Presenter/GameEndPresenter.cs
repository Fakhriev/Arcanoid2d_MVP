using UnityEngine;
using Model;
using TMPro;

namespace Presenter
{
    public class GameEndPresenter
    {
        private IWorldObjectGenerator[] _worldObjectsGenerators;

        private GameObject _gameEndModal;
        private TextMeshProUGUI _tmpPoints;

        private Ship _playerModel;

        private PointsCalculator _pointsCalculator;
        private int _points;

        public GameEndPresenter(IWorldObjectGenerator[] worldObjectsGenerators, GameObject gameEndModal, TextMeshProUGUI tmpPoints, Ship playerModel)
        {
            _worldObjectsGenerators = worldObjectsGenerators;

            _gameEndModal = gameEndModal;
            _tmpPoints = tmpPoints;

            _playerModel = playerModel;
            _pointsCalculator = new PointsCalculator();
            _points = 0;
        }

        public void Activate()
        {
            _playerModel.onDestroy += OnPlayerDestroy;

            for (int i = 0; i < _worldObjectsGenerators.Length; i++)
            {
                _worldObjectsGenerators[i].onModelGenerate += ObserveModelDestroy;
            }
        }

        private void OnPlayerDestroy(WorldObject model)
        {
            model.onDestroy -= OnPlayerDestroy;

            _tmpPoints.text = $"Points: {_points}";
            _gameEndModal.SetActive(true);
        }

        private void ObserveModelDestroy(WorldObject model)
        {
            model.onDestroy += OnModelDestroy;
        }

        private void OnModelDestroy(WorldObject model)
        {
            model.onDestroy -= OnModelDestroy;
            _points += _pointsCalculator.GetPoints((dynamic)model);
        }
    }
}