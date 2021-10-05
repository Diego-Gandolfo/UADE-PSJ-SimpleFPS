using SimpleFPS.Cameras;
using SimpleFPS.Life;
using SimpleFPS.Movement;
using SimpleFPS.Weapons;
using System;
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
        private HealthComponent _healthComponent;

        #endregion

        #region Propertys

        public IWeapon CurrentWeapon => _weaponController.CurrentWeapon;

        #endregion

        #region Events

        public event Action<bool> OnWalk, OnRun, OnSneak;
        public event Action<IWeapon> OnReload, OnAttack, OnChangeWeapon;
        public event Action OnInspect, OnHolster, OnKnifeAttack1, OnKnifeAttack2, OnThrowGrenade;
        public event Action OnAimOn, OnAimOff, OnSliderOutOfAmmo, OnSliderAmmoLeft;

        public event Action OnRecieveDamage;

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

            _healthComponent = GetComponent<HealthComponent>();
            _healthComponent.OnRecieveDamage += DoRecieveDamageHandler;
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

        public void DoWeaponAimOn()
        {
            OnAimOn?.Invoke();
        }

        public void DoWeaponAimOff()
        {
            OnAimOff?.Invoke();
        }

        public void DoWeaponReload()
        {
            OnReload?.Invoke(_weaponController.CurrentWeapon);
        }

        public void DoWeaponAttack()
        {
            if (((IGun)_weaponController.CurrentWeapon).CurrentMagazineAmmo > 0)
            {
                OnAttack?.Invoke(_weaponController.CurrentWeapon);
            }
            else
            {
                DoWeaponReload();
            }
        }

        public void DoWeaponInspect()
        {
            OnInspect?.Invoke();
        }

        public void DoWeaponHolster()
        {
            OnHolster?.Invoke();
        }

        public void DoWeaponKnifeAttack1()
        {
            OnKnifeAttack1?.Invoke();
        }

        public void DoWeaponKnifeAttack2()
        {
            OnKnifeAttack2?.Invoke();
        }

        public void DoWeaponThrowGrenade()
        {
            OnThrowGrenade?.Invoke();
        }

        public void DoRecieveDamageHandler()
        {
            OnRecieveDamage?.Invoke();
        }

        #endregion
    }
}
