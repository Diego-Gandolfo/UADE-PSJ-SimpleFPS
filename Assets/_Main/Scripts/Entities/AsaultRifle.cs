using Assets._Main.Scripts.Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Entities
{
    public class AsaultRifle : BaseGun
    {
        #region Private Fields

        private float _cooldownTimer;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
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

        public override void Attack(IWeaponController weaponController)
        {
            if (_currentMagazineAmmo > 0)
            {
                _canAttack = false;

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

                _cooldownTimer = _baseGunStats.FireCooldown;
            }
        }

        public override void Reload()
        {
            base.Reload();
        }

        #endregion
    }
}
