using SimpleFPS.Strategy.Weapons;
using UnityEngine;

namespace SimpleFPS.Entities.Weapons
{
    public class Handgun : BaseGun
    {
        #region Public Methods

        public override void Attack(IWeaponController weaponController)
        {
            if (_currentMagazineAmmo > 0)
            {
                Bullet bullet = weaponController.BulletPool.GetInstance();
                bullet.SetWeaponControlller(weaponController);
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

        #endregion
    }
}
