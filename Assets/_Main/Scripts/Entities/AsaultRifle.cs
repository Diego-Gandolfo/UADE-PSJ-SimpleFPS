using SimpleFPS.Generics.Pool;
using SimpleFPS.Projectiles;
using UnityEngine;

namespace SimpleFPS.Weapons
{
    public class AsaultRifle : BaseGun
    {
        #region Private Fields

        private float _cooldownTimer;
        private Pool<Bullet> _bulletPool;

        #endregion

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();
            _canAttack = true;
        }

        private void Update()
        {
            if (_cooldownTimer <= 0f)
            {
                _canAttack = true;
            }
            else
            {
                _cooldownTimer -= Time.deltaTime;
            }
        }

        #endregion

        #region Public Methods

        public override void Attack()
        {
            if (_currentMagazineAmmo > 0)
            {
                _canAttack = false;
                
                Bullet bullet = _bulletPool.GetInstance();
                bullet.SetBulletPool(_bulletPool);
                bullet.transform.position = _bulletSpawnpoint.position;
                bullet.transform.rotation = _bulletSpawnpoint.rotation;
                bullet.SetDamage(Damage);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * BULLET_FORCE;

                _currentMagazineAmmo--;
                _magazineAmmoText.text = _currentMagazineAmmo.ToString();

                _muzzleFlashLight.enabled = true;
                Invoke("TurnMuzzleFlashLightOff", 0.02f);
                PlayMuzzleFlashParticles();
                PlaySparkParticles();

                _cooldownTimer = _baseGunStats.FireCooldown;
            }
        }

        public override void Reload()
        {
            base.Reload();
        }

        public override void SetBulletPool(Pool<Bullet> bulletPool)
        {
            _bulletPool = bulletPool;
        }

        #endregion
    }
}
