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

        #endregion

        #region Unity Methods

        private void OnCollisionEnter(Collision collision)
        {
            if ((_layerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                EnemyManager.Instance.AddCommand(new CmdExplosion(transform.position, transform.rotation));
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
