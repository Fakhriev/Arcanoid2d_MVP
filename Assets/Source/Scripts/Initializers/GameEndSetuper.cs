using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Model;
using Presenter;
using TMPro;

namespace Setuper
{
    public class GameEndSetuper : MonoBehaviour
    {
        [SerializeField] private GameObject _gameEndModal;
        [SerializeField] private TextMeshProUGUI _tmpPoints;
        [SerializeField] private Button _btnRestart;

        private Ship _playerModel;
        private GameEndPresenter _gameEndPresenter;

        public void Init(IWorldObjectGenerator[] worldObjectsGenerators, Ship playerModel)
        {
            _playerModel = playerModel;

            _gameEndPresenter = new GameEndPresenter(worldObjectsGenerators, _gameEndModal, _tmpPoints, _playerModel);
            _gameEndPresenter.Activate();

            _btnRestart.onClick.AddListener(LoadPreviewScene);
        }

        private void LoadPreviewScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}