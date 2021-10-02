using UnityEngine;

namespace SimpleFPS.Components
{
    public class AutoDestroy : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private float _timeToDestroy = 0f;

        #endregion

        #region Private Fields

        private float _timeCounter;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _timeCounter = _timeToDestroy;
        }

        private void Update()
        {
            _timeCounter -= Time.deltaTime;

            if (_timeCounter <= 0f) Destroy(gameObject);
        }

        #endregion
    }
}
