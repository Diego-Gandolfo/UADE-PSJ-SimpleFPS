using SimpleFPS.Life;
using UnityEngine;

namespace SimpleFPS.Components
{
    public class Explosion : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private float _timeToDespawn = 1f;
        [SerializeField] private float _radius = 1f;
        [SerializeField] private float _damage = 1f;
        [SerializeField] private LayerMask _layerMask;

        #endregion

        #region Private Fields

        private float _timer;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _timer = _timeToDespawn;
            DoExplotion();
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                Managers.LevelManager.Instance.ExplotionPool.StoreInstance(this);
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawSphere(transform.position, _radius);
        }

        #endregion

        #region Private Methods

        private void DoExplotion()
        {
            var hits = Physics.OverlapSphere(transform.position, _radius, _layerMask);

            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    var health = hit.GetComponent<HealthComponent>();

                    if (health != null)
                    {
                        health.ReceiveDamage(_damage);
                    }
                }
            }
        }

        #endregion
    }
}