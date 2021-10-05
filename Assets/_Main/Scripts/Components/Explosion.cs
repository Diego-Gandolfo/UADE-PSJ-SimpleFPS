using SimpleFPS.Life;
using SimpleFPS.Sounds;
using UnityEngine;

namespace SimpleFPS.Components
{
    public class Explosion : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private AudioSource _mainAudioSource;
        [SerializeField] private FXSounds _sounds;
        [SerializeField] private float _timeToDespawn = 1f;
        [SerializeField] private float _radius = 1f;
        [SerializeField] private float _damage = 1f;
        [SerializeField] private LayerMask _layerMask;

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
                Managers.LevelManager.Instance.ExplosionFactory.StoreExplosion(this);
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawSphere(transform.position, _radius);
        }

        #endregion

        #region Public Methods

        public void DoExplotion()
        {
            var explosionSound = _sounds.ExplosionSounds[Random.Range(0, _sounds.ExplosionSounds.Count)];
            _mainAudioSource.PlayOneShot(explosionSound);

            var hits = Physics.OverlapSphere(transform.position, _radius, _layerMask);
            print($"Position: {transform.position} - Radio: {_radius} - Hits: {hits.Length}");
            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    var health = hit.GetComponent<Health>();

                    if (health != null)
                    {
                        print(health.gameObject.name);
                        health.ReceiveDamage(_damage);
                    }
                }
            }
        }

        #endregion
    }
}
