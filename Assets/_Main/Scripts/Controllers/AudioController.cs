using Assets._Main.Scripts.Strategy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Controllers
{
    public class AudioController : MonoBehaviour
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
        [SerializeField] private AudioClip _holsterWeaponSound;
        [SerializeField] private AudioClip _takeOutGunSound;
        [SerializeField] private AudioClip _knifeAttackSound;
        [SerializeField] private AudioClip _throwGrenadeSound;

        #endregion

        #region Private Methods

        private void OnWalkHandler(bool value)
        {
            if (value)
            {
                _movementAudioSource.clip = _walkSound;

                if (!_movementAudioSource.isPlaying)
                {
                    _movementAudioSource.Play();
                }
            }
        }

        private void OnRunHandler(bool value)
        {
            if (value)
            {
                _movementAudioSource.clip = _runSound;

                if (!_movementAudioSource.isPlaying)
                {
                    _movementAudioSource.Play();
                }
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
                if (currentWeapon is BaseGunController)
                {
                    if (((BaseGunController)currentWeapon).IsMagazineEmpty)
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
            if (currentWeapon is HandgunController)
            {
                _mainWeaponAudioSource.clip = _takeOutGunSound;
                _mainWeaponAudioSource.Play();
            }
            else if (currentWeapon is AsaultRifleController)
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
            //if (currentWeapon is IGun)
            //{
            _mainWeaponAudioSource.clip = _throwGrenadeSound;
            _mainWeaponAudioSource.Play();
            //}
        }

        private void OnKnifeAttack2Handler()
        {
            //if (currentWeapon is IGun)
            //{
            _mainWeaponAudioSource.clip = _knifeAttackSound;
            _mainWeaponAudioSource.Play();
            //}
        }

        private void OnKnifeAttack1Handler()
        {
            //if (currentWeapon is IGun)
            //{
            _mainWeaponAudioSource.clip = _knifeAttackSound;
            _mainWeaponAudioSource.Play();
            //}
        }

        #endregion

        #region Public Methods

        public void SuscribeEvents(IInputController inputController)
        {
            inputController.OnReload += OnReloadHandler;
            inputController.OnAttack += OnAttackHandler;
            inputController.OnChangeWeapon += OnChangeWeaponHandler;
            inputController.OnAimOn += OnAimOnHandler;
            inputController.OnHolster += OnHolsterHandler;
            inputController.OnRun += OnRunHandler;
            inputController.OnWalk += OnWalkHandler;
            inputController.OnKnifeAttack1 += OnKnifeAttack1Handler;
            inputController.OnKnifeAttack2 += OnKnifeAttack2Handler;
            inputController.OnThrowGrenade += OnThrowGrenadeHandler;
        }

        #endregion
    }
}