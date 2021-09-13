using Assets._Main.Scripts.Component;
using Assets._Main.Scripts.Strategy;
using UnityEngine;

namespace Assets._Main.Scripts.Entities
{
    public class Bullet : MonoBehaviour, IBullet
    {
        #region Serialize Fields

        [SerializeField] private float _timeToDestroy = 1f;

        #endregion

        #region Private Fields

        private IWeaponController _weaponController;
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
                _weaponController.BulletPool.StoreInstance(this);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            var heatlhComponent = collision.gameObject.GetComponent<HealthComponent>();
            if (heatlhComponent != null) heatlhComponent.ReceiveDamage(Damage);
            //print($"Bullet: {_weaponController}");
            BulletImpact bulletImpact = _weaponController.BulletImpactPool.GetInstance();
            bulletImpact.SetWeaponControlller(_weaponController);
            bulletImpact.transform.position = transform.position;
            bulletImpact.transform.rotation = transform.rotation;
            _weaponController.BulletPool.StoreInstance(this);
        }

        #endregion

        #region Public Methods

        public void SetWeaponControlller(IWeaponController weaponController)
        {
            _weaponController = weaponController;
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        #endregion
    }
}
