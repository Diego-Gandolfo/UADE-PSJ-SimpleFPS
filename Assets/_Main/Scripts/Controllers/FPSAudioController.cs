using SimpleFPS.Player;
using SimpleFPS.Weapons;
using UnityEngine;

namespace SimpleFPS.FPS
{
    public class FPSAudioController : MonoBehaviour, IFPSController
    {
        #region Serialize Fields

        [Header("AudioSources")]
        [SerializeField] private AudioSource _movementAudioSource;
        [SerializeField] private AudioSource _mainWeaponAudioSource;
        [SerializeField] private AudioSource _attackWeaponAudioSource;

        [Header("Sounds")]
        [SerializeField] private AudioClip _shootSound;
        [SerializeField] private AudioClip _aimSound;
        [SerializeField] private AudioClip _reloadAmmoLeftSound;
        [SerializeField] private AudioClip _reloadOutOfAmmoSound;
        [SerializeField] private AudioClip _walkSound;
        [SerializeField] private AudioClip _runSound;
        [SerializeField] private AudioClip _sneakSound;
        [SerializeField] private AudioClip _holsterWeaponSound;
        [SerializeField] private AudioClip _takeOutGunSound;
        [SerializeField] private AudioClip _knifeCutAttackSound;
        [SerializeField] private AudioClip _knifeSlabAttackSound;
        [SerializeField] private AudioClip _throwGrenadeSound;

        #endregion

        #region Private Methods

        private void OnWalkHandler(bool value)
        {
            if (value)
            {
                _movementAudioSource.clip = _walkSound;
                _movementAudioSource.volume = 0.075f;

                if (_movementAudioSource.clip != _walkSound) _movementAudioSource.Stop();

                if (!_movementAudioSource.isPlaying)
                {
                    _movementAudioSource.Play();
                }
            }
            else
            {
                if (_movementAudioSource.clip == _walkSound) _movementAudioSource.Stop();
            }
        }

        private void OnRunHandler(bool value)
        {
            if (value)
            {
                _movementAudioSource.clip = _runSound;
                _movementAudioSource.volume = 1f;

                if (_movementAudioSource.clip != _runSound) _movementAudioSource.Stop();

                if (!_movementAudioSource.isPlaying)
                {
                    _movementAudioSource.Play();
                }
            }
            else
            {
                if (_movementAudioSource.clip == _runSound) _movementAudioSource.Stop();
            }
        }

        private void OnSneakHandler(bool value)
        {
            if (value)
            {
                _movementAudioSource.clip = _sneakSound;
                _movementAudioSource.volume = 0.05f;

                if (_movementAudioSource.clip != _sneakSound) _movementAudioSource.Stop();

                if (!_movementAudioSource.isPlaying)
                {
                    _movementAudioSource.Play();
                }
            }
            else
            {
                if (_movementAudioSource.clip == _sneakSound) _movementAudioSource.Stop();
            }
        }

        private void OnAttackHandler(IWeapon currentWeapon)
        {
            if (currentWeapon is IGun)
            {
                if (!((IGun)currentWeapon).IsMagazineEmpty)
                {
                    _attackWeaponAudioSource.clip = _shootSound;
                    _attackWeaponAudioSource.Play();
                }
            }
        }

        private void OnReloadHandler(IWeapon currentWeapon)
        {
            if (currentWeapon is IGun)
            {
                if (currentWeapon is BaseGun)
                {
                    if (((BaseGun)currentWeapon).IsMagazineEmpty)
                    {
                        _mainWeaponAudioSource.clip = _reloadOutOfAmmoSound;
                        _mainWeaponAudioSource.Play();
                    }
                    else
                    {
                        _mainWeaponAudioSource.clip = _reloadAmmoLeftSound;
                        _mainWeaponAudioSource.Play();
                    }
                }
            }
        }

        private void OnChangeWeaponHandler(IWeapon currentWeapon)
        {
            if (currentWeapon is Handgun)
            {
                _mainWeaponAudioSource.clip = _takeOutGunSound;
                _mainWeaponAudioSource.Play();
            }
            else if (currentWeapon is AsaultRifle)
            {
                _mainWeaponAudioSource.clip = _reloadAmmoLeftSound;
                _mainWeaponAudioSource.Play();
            }
        }

        private void OnHolsterHandler()
        {
            //if (currentWeapon is IGun)
            //{
                _mainWeaponAudioSource.clip = _holsterWeaponSound;
                _mainWeaponAudioSource.Play();
            //}
        }

        private void OnAimOnHandler()
        {
            //if (currentWeapon is IGun)
            //{
                _mainWeaponAudioSource.clip = _aimSound;
                _mainWeaponAudioSource.Play();
            //}
        }

        private void OnThrowGrenadeHandler()
        {
            _mainWeaponAudioSource.clip = _throwGrenadeSound;
            _mainWeaponAudioSource.Play();
        }

        private void OnKnifeAttack2Handler()
        {
            _attackWeaponAudioSource.clip = _knifeCutAttackSound;
            _attackWeaponAudioSource.Play();
        }

        private void OnKnifeAttack1Handler()
        {
            _attackWeaponAudioSource.clip = _knifeCutAttackSound;
            _attackWeaponAudioSource.Play();
            _mainWeaponAudioSource.clip = _knifeSlabAttackSound;
            _mainWeaponAudioSource.PlayDelayed(0.472f);
        }

        #endregion

        #region Public Methods

        public void SuscribeEvents(FPSCharacterController characterController)
        {
            characterController.OnReload += OnReloadHandler;
            characterController.OnAttack += OnAttackHandler;
            characterController.OnChangeWeapon += OnChangeWeaponHandler;
            characterController.OnAimOn += OnAimOnHandler;
            characterController.OnHolster += OnHolsterHandler;
            characterController.OnSneak += OnSneakHandler;
            characterController.OnRun += OnRunHandler;
            characterController.OnWalk += OnWalkHandler;
            characterController.OnKnifeAttack1 += OnKnifeAttack1Handler;
            characterController.OnKnifeAttack2 += OnKnifeAttack2Handler;
            characterController.OnThrowGrenade += OnThrowGrenadeHandler;
        }

        #endregion
    }
}
