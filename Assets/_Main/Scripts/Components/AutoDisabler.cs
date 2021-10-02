using UnityEngine;

namespace SimpleFPS.Components
{
    public class AutoDisabler : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private float _timeToDisable = 0f;

        #endregion

        #region Private Fields

        private float _timeCounter;

        #endregion

        #region Propertys

        public float TimeToDisable => _timeToDisable;

        #endregion

        #region Unity Methods

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _timeCounter = _timeToDisable;
        }

        private void Update()
        {
            _timeCounter -= Time.deltaTime;

            if (_timeCounter <= 0f) gameObject.SetActive(false);
        }

        #endregion
    }
}
