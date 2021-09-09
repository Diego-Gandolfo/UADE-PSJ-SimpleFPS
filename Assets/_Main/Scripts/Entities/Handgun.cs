using Assets._Main.Scripts.Component;
using Assets._Main.Scripts.Generics;
using Assets._Main.Scripts.Strategy;
using UnityEngine;

namespace Assets._Main.Scripts.Entities
{
    public class Handgun : BaseGun
    {
        #region Serialize Fields

        //[Header("Spawnpoints")]
        //[SerializeField] private Transform _bulletSpawnpoint;

        //[Header("Sparks")]
        //[SerializeField] private ParticleSystem _sparkParticles;
        //[SerializeField] private int _minSparks = 1;
        //[SerializeField] private int _maxSparks = 7;

        //[Header("Muzzle Flash")]
        //[SerializeField] private ParticleSystem _muzzleFlashParticles;
        //[SerializeField] private Light _muzzleFlashLight;

        #endregion

        #region Private Fields

        //private const float BULLET_FORCE = 400f;

        #endregion

        #region Private Methods

        //private void TurnMuzzleFlashLightOff()
        //{
        //    _muzzleFlashLight.enabled = false;
        //}

        //private void PlayMuzzleFlashParticles()
        //{
        //    _muzzleFlashParticles.Emit(3);
        //}

        //private void PlaySparkParticles()
        //{
        //    _sparkParticles.Emit(Random.Range(_minSparks, _maxSparks));
        //}

        #endregion

        #region Public Methods

        public override void Attack(IWeaponController weaponController)
        {
            //base.Attack();

            if (_currentMagazineAmmo > 0)
            {
                //Bullet bullet = Instantiate(_baseGunStats.BulletPrefab, _bulletSpawnpoint.position, _bulletSpawnpoint.rotation);
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
