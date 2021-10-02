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

        private float _timeCounter;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _timeCounter = _timeToStore;
        }

        private void Update()
        {
            _timeCounter -= Time.deltaTime;

            if (_timeCounter <= 0f) Destroy(gameObject);
        }

        #endregion
    }
}
