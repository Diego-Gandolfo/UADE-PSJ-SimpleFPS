using SimpleFPS.Managers;
using UnityEngine;

namespace SimpleFPS.Components
{
    public class AutoPoolStore : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private MonoBehaviour _type;
        [SerializeField] private float _timeToStore = 0f;

        #endregion

        #region Private Fields

        private GameManager _gameManager;
        private float _timeCounter;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _timeCounter = _timeToStore;
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
