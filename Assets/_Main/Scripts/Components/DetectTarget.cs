using System;
using UnityEngine;

namespace SimpleFPS.Patrol
{
    public class DetectTarget : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private LayerMask _layerMask = 0;

        #endregion

        #region Events

        public event Action OnDetection;

        #endregion

        #region Unity Methods

        private void OnTriggerEnter(Collider other)
        {
            if ((_layerMask.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                OnDetection?.Invoke();
            }
        }

        #endregion
    }
}
