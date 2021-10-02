using SimpleFPS.Command;
using SimpleFPS.Components;
using System;
using UnityEngine;

namespace SimpleFPS.Enemy
{
    public class ExploteOnCollision : MonoBehaviour
    {
        #region Serialize Fields

        [Header("Target")]
        [SerializeField] private LayerMask _layerMask = 0;

        [Header("Particles")]
        [SerializeField] private Explotion _explotionPrefab;

        #endregion

        #region Events

        public event Action OnExplotion;

        #endregion

        #region Unity Methods

        private void OnCollisionEnter(Collision collision)
        {
            if ((_layerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                var explotion = Managers.LevelManager.Instance.ExplotionPool.GetInstance();
                explotion.transform.position = transform.position;
                explotion.transform.rotation = transform.rotation;
                OnExplotion?.Invoke();
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
