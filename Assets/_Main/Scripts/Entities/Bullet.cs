using SimpleFPS.Damageable;
using SimpleFPS.Generics.Pool;
using SimpleFPS.LevelManagers;
using UnityEngine;

namespace SimpleFPS.Projectiles
{
    public class Bullet : MonoBehaviour, IBullet
    {
        #region Serialize Fields

        [SerializeField] private float _timeToDestroy = 1f;

        #endregion

        #region Private Fields

        private float _timer;
        private float _damage;
        private Pool<Bullet> _bulletPool;
        private Pool<BulletImpact> _bulletImpactPool;

        #endregion

        #region Propertys

        public float Damage => _damage;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            _timer = _timeToDestroy;
        }

        private void Start()
        {
            _bulletImpactPool = LevelManager.Instance.BulletImpactPool;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                _bulletPool.StoreInstance(this);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            var heatlhComponent = collision.gameObject.GetComponent<HealthComponent>();

            if (heatlhComponent != null)
            {
                heatlhComponent.ReceiveDamage(Damage);
            }
            else
            {
                BulletImpact bulletImpact = _bulletImpactPool.GetInstance();
                bulletImpact.transform.position = transform.position;
                bulletImpact.transform.rotation = transform.rotation;
            }

            _bulletPool.StoreInstance(this);
        }

        #endregion

        #region Public Methods

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        public void SetBulletPool(Pool<Bullet> bulletPool)
        {
            _bulletPool = bulletPool;
        }

        #endregion
    }
}
