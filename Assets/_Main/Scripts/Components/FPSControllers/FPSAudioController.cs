using SimpleFPS.Player;
using SimpleFPS.Sounds;
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
        [SerializeField] private FXSounds _sounds;

        #endregion

        #region Private Methods

        private void OnWalkHandler(bool value)
        {
            if (value)
            {
                _movementAudioSource.clip = _sounds.WalkSound;
                _movementAudioSource.volume = 0.075f;

                if (_movementAudioSource.clip != _sounds.WalkSound) _movementAudioSource.Stop();

                if (!_movementAudioSource.isPlaying)
                {
                    _movementAudioSource.Play();
                }
            }
            else
            {
                if (_movementAudioSource.clip == _sounds.WalkSound) _movementAudioSource.Stop();
            }
        }

        private void OnRunHandler(bool value)
        {
            if (value)
            {
                _movementAudioSource.clip = _sounds.RunSound;
                _movementAudioSource.volume = 1f;

                if (_movementAudioSource.clip != _sounds.RunSound) _movementAudioSource.Stop();

                if (!_movementAudioSource.isPlaying)
                {
                    _movementAudioSource.Play();
                }
            }
            else
            {
                if (_movementAudioSource.clip == _sounds.RunSound) _movementAudioSource.Stop();
            }
        }

        private void OnSneakHandler(bool value)
        {
            if (value)
            {
                _movementAudioSource.clip = _sounds.SneakSound;
                _movementAudioSource.volume = 0.05f;

                if (_movementAudioSource.clip != _sounds.SneakSound) _movementAudioSource.Stop();

                if (!_movementAudioSource.isPlaying)
                {
                    _movementAudioSource.Play();
                }
            }
            else
            {
                if (_movementAudioSource.clip == _sounds.SneakSound) _movementAudioSource.Stop();
            }
        }

        private void OnAttackHandler(IWeapon currentWeapon)
        {
            if (currentWeapon is IGun)
            {
                if (!((IGun)currentWeapon).IsMagazineEmpty)
                {
                    _attackWeaponAudioSource.clip = _sounds.ShootSound;
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
                        _mainWeaponAudioSource.clip = _sounds.ReloadOutOfAmmoSound;
                        _mainWeaponAudioSource.Play();
                    }
                    else
                    {
                        _mainWeaponAudioSource.clip = _sounds.ReloadAmmoLeftSound;
                        _mainWeaponAudioSource.Play();
                    }
                }
            }
        }

        private void OnChangeWeaponHandler(IWeapon currentWeapon)
        {
            if (currentWeapon is Handgun)
            {
                _mainWeaponAudioSource.clip = _sounds.TakeOutGunSound;
                _mainWeaponAudioSource.Play();
            }
            else if (currentWeapon is AsaultRifle)
            {
                _mainWeaponAudioSource.clip = _sounds.ReloadAmmoLeftSound;
                _mainWeaponAudioSource.Play();
            }
        }

        private void OnHolsterHandler()
        {
            _mainWeaponAudioSource.clip = _sounds.HolsterWeaponSound;
            _mainWeaponAudioSource.Play();
        }

        private void OnAimOnHandler()
        {
            _mainWeaponAudioSource.clip = _sounds.AimSound;
            _mainWeaponAudioSource.Play();
        }

        private void OnKnifeAttack2Handler()
        {
            _attackWeaponAudioSource.clip = _sounds.KnifeCutAttackSound;
            _attackWeaponAudioSource.Play();
        }

        private void OnKnifeAttack1Handler()
        {
            _attackWeaponAudioSource.clip = _sounds.KnifeCutAttackSound;
            _attackWeaponAudioSource.Play();
            _mainWeaponAudioSource.clip = _sounds.KnifeStabAttackSound;
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
        }

        #endregion
    }
}
