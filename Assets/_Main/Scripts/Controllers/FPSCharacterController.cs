using SimpleFPS.Cameras;
using SimpleFPS.Movement;
using SimpleFPS.Player;
using SimpleFPS.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.FPS
{
    public class FPSCharacterController : MonoBehaviour
    {
        #region Private Fields

        // Components
        private MoveComponent _moveComponent;
        private RotationComponent _rotationComponent;
        private JumpComponent _jumpComponent;
        private FPSWeaponsController _weaponController;
        private FPSAnimationsController _animationsController;
        private FPSCameraController _cameraController;
        private FPSAudioController _audioController;

        #endregion

        #region Events

        public event Action<bool> OnWalk, OnRun, OnSneak;
        public event Action<IWeapon> OnReload, OnAttack, OnChangeWeapon;
        public event Action OnInspect, OnHolster, OnKnifeAttack1, OnKnifeAttack2, OnThrowGrenade;
        public event Action OnAimOn, OnAimOff, OnSliderOutOfAmmo, OnSliderAmmoLeft;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            GetRequiredComponent();
        }

        private void Update()
        {
            CheckAmmoSlider();
        }

        #endregion

        #region Private Methods

        private void GetRequiredComponent()
        {
            _weaponController = GetComponent<FPSWeaponsController>();
            _weaponController.SuscribeEvents(this);

            _animationsController = GetComponent<FPSAnimationsController>();
            _animationsController.SuscribeEvents(this);

            _audioController = GetComponent<FPSAudioController>();
            _audioController.SuscribeEvents(this);

            OnChangeWeapon?.Invoke(_weaponController.WeaponList[0]);

            _moveComponent = GetComponent<MoveComponent>();
            _rotationComponent = GetComponent<RotationComponent>();
            _jumpComponent = GetComponent<JumpComponent>();

            _cameraController = GetComponent<FPSCameraController>();
            _cameraController.SuscribeEvents(this);
        }

        private void CheckAmmoSlider()
        {
            if (_weaponController.CurrentWeapon is Handgun)
            {
                if (!((IGun)_weaponController.CurrentWeapon).IsMagazineEmpty && _animationsController.Animator.GetBool("Out Of Ammo Slider"))
                    OnSliderAmmoLeft?.Invoke();
                if (((IGun)_weaponController.CurrentWeapon).IsMagazineEmpty && !_animationsController.Animator.GetBool("Out Of Ammo Slider"))
                    OnSliderOutOfAmmo?.Invoke();
            }
        }

        #endregion

        #region Public Methods

        public void DoMovement(string movementType, Vector3 direction)
        {
            float currentSpeed;

            if (movementType == "Run")
            {
                currentSpeed = _moveComponent.RunSpeed;
            }
            else if (movementType == "Sneak")
            {
                currentSpeed = _moveComponent.SneakSpeed;
            }
            else
            {
                currentSpeed = _moveComponent.WalkSpeed;
            }


            _moveComponent.DoMove(direction, currentSpeed);

            OnWalk?.Invoke(direction != Vector3.zero && currentSpeed == _moveComponent.WalkSpeed);
            OnRun?.Invoke(direction != Vector3.zero && currentSpeed == _moveComponent.RunSpeed);
            OnSneak?.Invoke(direction != Vector3.zero && currentSpeed == _moveComponent.SneakSpeed);
        }

        public void DoRotation(float mouseInput)
        {
            _rotationComponent.Rotate(mouseInput);
        }

        public void DoLookUpDown(float mouseInput)
        {
            _cameraController.LookUpDown(mouseInput);
        }

        public void DoJump()
        {
            if (_jumpComponent.CheckIsGrounded())
            {
                _jumpComponent.DoJump();
            }
        }

        public void DoWeaponChange(int input)
        {
            if (!_weaponController.CurrentWeapon.Equals(_weaponController.WeaponList[input]))
            {
                if (input == 0)
                    OnChangeWeapon?.Invoke(_weaponController.WeaponList[0]);
                if (input == 1)
                    OnChangeWeapon?.Invoke(_weaponController.WeaponList[1]);
            }
        }

        public void DoWeapon()
        {

            //if (Input.GetMouseButtonDown((int)_aimMouseButton)) OnAimOn?.Invoke();
            //if (Input.GetMouseButtonUp((int)_aimMouseButton)) OnAimOff?.Invoke();

            //if (Input.GetKeyDown(KeyCode.R)) OnReload?.Invoke(_weaponController.CurrentWeapon);

            //if (((IGun)_weaponController.CurrentWeapon).IsAutomatic)
            //{
            //    if (Input.GetMouseButton((int)_attackMouseButton) && ((IGun)_weaponController.CurrentWeapon).CanAttack)
            //        OnAttack?.Invoke(_weaponController.CurrentWeapon);
            //}
            //else
            //{
            //    if (Input.GetMouseButtonDown((int)_attackMouseButton)) OnAttack?.Invoke(_weaponController.CurrentWeapon);
            //}

            //if (Input.GetKeyDown(_inspectKey)) OnInspect?.Invoke();

            //if (Input.GetKeyDown(_holsterKey)) OnHolster?.Invoke();

            //if (Input.GetKeyDown(_knifeAttack1Key)) OnKnifeAttack1?.Invoke();
            //if (Input.GetKeyDown(_knifeAttack2Key)) OnKnifeAttack2?.Invoke();

            //if (Input.GetKeyDown(_granadeKey)) OnThrowGrenade?.Invoke();
        }

        #endregion
    }
}
