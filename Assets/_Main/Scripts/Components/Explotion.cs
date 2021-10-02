using UnityEngine;

namespace SimpleFPS.Components
{
    public class Explotion : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private float _timeToDespawn = 1f;

        #endregion

        #region Private Fields

        private float _timer;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            _timer = _timeToDespawn;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                Managers.LevelManager.Instance.ExplotionPool.StoreInstance(this);
            }
        }

        #endregion
    }
}
