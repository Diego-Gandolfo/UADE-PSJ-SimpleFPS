using Assets._Main.Scripts.Component;
using SimpleFPS.LevelManagers;
using UnityEngine;

namespace SimpleFPS.Projectiles.Bullets
{
    public class Bullet : MonoBehaviour, IBullet
    {
        #region Serialize Fields

        [SerializeField] private float _timeToDestroy = 1f;

        #endregion

        #region Private Fields

        private float _timer;
        private float _damage;

        #endregion

        #region Propertys

        public float Damage => _damage;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            _timer = _timeToDestroy;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                LevelManager.Instance.PlayerBulletPool.StoreInstance(this);
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
                BulletImpact bulletImpact = LevelManager.Instance.BulletImpactPool.GetInstance();
                bulletImpact.transform.position = transform.position;
                bulletImpact.transform.rotation = transform.rotation;
            }

            LevelManager.Instance.PlayerBulletPool.StoreInstance(this);
        }

        #endregion

        #region Public Methods

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        #endregion
    }
}
