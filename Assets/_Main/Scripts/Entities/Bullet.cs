using SimpleFPS.Life;
using SimpleFPS.Generics.Pool;
using UnityEngine;
using SimpleFPS.Factory;

namespace SimpleFPS.Projectiles
{
    public class Bullet : MonoBehaviour, IBullet
    {
        #region Serialize Fields

        [SerializeField] private float _timeToDespawn = 1f;

        #endregion

        #region Private Fields

        // Stats
        private BulletStats _bulletStats;

        // Parameters
        private float _timer;
        private float _damage;
        private bool _canCount;

        #endregion

        #region Propertys

        public float Damage => _damage;
        public BulletStats BulletStats => _bulletStats;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            _timer = _timeToDespawn;
            _canCount = true;
        }

        private void Update()
        {
            if (_canCount)
            {
                if (_timer <= 0f)
                {
                    //Managers.LevelManager.Instance.BulletPool.StoreInstance(this);
                    Managers.LevelManager.Instance.BulletFactory.StoreBullet(this);
                }
                else
                {
                    _timer -= Time.deltaTime;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((_bulletStats.TargetsLayers & 1 << collision.gameObject.layer) != 0)
            {
                var heatlhComponent = collision.gameObject.GetComponent<HealthComponent>();

                if (heatlhComponent != null)
                {
                    heatlhComponent.ReceiveDamage(Damage);
                }
                else
                {
                    BulletImpact bulletImpact = Managers.LevelManager.Instance.BulletImpactPool.GetInstance();
                    bulletImpact.transform.position = transform.position;
                    bulletImpact.transform.rotation = transform.rotation;
                }

                _canCount = false;
                //Managers.LevelManager.Instance.BulletPool.StoreInstance(this);
                Managers.LevelManager.Instance.BulletFactory.StoreBullet(this);
            }
        }

        #endregion

        #region Public Methods

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        public void SetStats(BulletStats stats)
        {
            _bulletStats = stats;
        }

        #endregion
    }
}
