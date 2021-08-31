using Assets._Main.Scripts.Strategy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Controllers
{
    public class AnimationsController : MonoBehaviour
    {
        #region Private Fields

        // Components
        private Animator _animator;

        #endregion

        #region Private Methods

        private void OnAimHandler(bool value)
        {
            if (!_animator.GetBool("Run"))
            {
                _animator.SetBool("Aim", value);
            }
        }

        private void OnKnifeAttack2Handler()
        {
            _animator.Play("Knife Attack 2", 0, 0f);
        }

        private void OnKnifeAttack1Handler()
        {
            _animator.Play("Knife Attack 1", 0, 0f);
        }

        private void OnThrowGrenadeHandler()
        {
            _animator.Play("GrenadeThrow", 0, 0.0f);
        }

        private void OnHolsterHandler()
        {
            _animator.SetBool("Holster", !_animator.GetBool("Holster"));
        }

        private void OnInspectHander()
        {
            _animator.SetTrigger("Inspect");
        }

        private void OnAttackHandler()
        {
            if (_animator.GetBool("Aim")) _animator.Play("Aim Fire", 0, 0f);
            else _animator.Play("Fire", 0, 0f);
        }

        private void OnReloadHandler()
        {
            // TODO: Pasar animaciones de Reload
        }

        private void OnChangeWeaponHandler(IWeapon baseWeaponController)
        {
            if (baseWeaponController is BaseWeaponController)
                _animator = ((BaseWeaponController)baseWeaponController).gameObject.GetComponent<Animator>();
        }

        private void OnRunHandler(bool value)
        {
            _animator.SetBool("Run", value);
        }

        private void OnWalkHandler(bool value)
        {
            _animator.SetBool("Walk", value);
        }

        #endregion

        #region Public Methods

        public void SuscribeEvents(IInputController characterBehaviour)
        {
            characterBehaviour.OnReload += OnReloadHandler;
            characterBehaviour.OnAttack += OnAttackHandler;
            characterBehaviour.OnInspect += OnInspectHander;
            characterBehaviour.OnHolster += OnHolsterHandler;
            characterBehaviour.OnThrowGrenade += OnThrowGrenadeHandler;
            characterBehaviour.OnKnifeAttack1 += OnKnifeAttack1Handler;
            characterBehaviour.OnKnifeAttack2 += OnKnifeAttack2Handler;
            characterBehaviour.OnAim += OnAimHandler;
            characterBehaviour.OnChangeWeapon += OnChangeWeaponHandler;
            characterBehaviour.OnWalk += OnWalkHandler;
            characterBehaviour.OnRun += OnRunHandler;
        }

        #endregion
    }
}
