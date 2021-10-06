using SimpleFPS.Managers;
using UnityEngine;

namespace SimpleFPS.Components
{
    public class AutoDestroy : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private float _timeToDestroy = 0f;

        #endregion

        #region Private Fields

        private GameManager _gameManager;
        private float _timeCounter;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _timeCounter = _timeToDestroy;
        }

        private void Update()
        {
            if (!_gameManager.IsPaused)
            {
                _timeCounter -= Time.deltaTime;

                if (_timeCounter <= 0f) Destroy(gameObject);
            }
        }

        #endregion
    }
}
