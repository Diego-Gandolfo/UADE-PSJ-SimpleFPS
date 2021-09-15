using SimpleFPS.Generics.Pool;
using SimpleFPS.Projectiles;
using UnityEngine;

namespace SimpleFPS.Weapons
{
    public class Handgun : BaseGun
    {
        #region Private Fields

        private Pool<Bullet> _bulletPool;

        #endregion

        #region Public Methods

        public override void Attack()
        {
            if (_currentMagazineAmmo > 0)
            {
                Bullet bullet = _bulletPool.GetInstance();
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
