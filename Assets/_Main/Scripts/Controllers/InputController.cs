using Assets._Main.Scripts.Component;
using Assets._Main.Scripts.Entities;
using Assets._Main.Scripts.Strategy;
using System;
using UnityEngine;

public enum MouseButton
{
    Left,
    Right,
    Middle
}

namespace Assets._Main.Scripts.Controllers
{
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(RotationComponent))]
    [RequireComponent(typeof(JumpComponent))]
    [RequireComponent(typeof(WeaponsController))]
    [RequireComponent(typeof(AnimationsController))]
    [RequireComponent(typeof(CameraController))]
    [RequireComponent(typeof(AudioController))]
    public class InputController : MonoBehaviour, IInputController
    {
        #region Serialize Fields

        [Header("Movement")]
        [SerializeField] private string _horizontalAxis = "Horizontal";
        [SerializeField] private string _verticalAxis = "Vertical";
        [SerializeField] private KeyCode _runKey = KeyCode.LeftShift;
        [SerializeField] private KeyCode _sneakKey = KeyCode.LeftControl;

        [Header("Rotation")]
        [SerializeField] private string _rotationAxis = "Mouse X";
        [SerializeField, Range(0, 1)] private float _xMouseSensibility = 0.5f;

        [Header("Jump")]
        [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

        [Header("Weapons")]
        [SerializeField] private MouseButton _attackMouseButton = MouseButton.Left;
        [SerializeField] private MouseButton _aimMouseButton = MouseButton.Right;
        [SerializeField] private KeyCode _inspectKey = KeyCode.T;
        [SerializeField] private KeyCode _holsterKey = KeyCode.F;
        [SerializeField] private KeyCode _knifeAttack1Key = KeyCode.Q;
        [SerializeField] private KeyCode _knifeAttack2Key = KeyCode.E;
        [SerializeField] private KeyCode _granadeKey = KeyCode.G;
        
        [Header("Look Up-Down")]
        [SerializeField] private string _lookUpDownAxis = "Mouse Y";
        [SerializeField, Range(0, 1)] private float _yMouseSensibility = 0.5f;

        #endregion

        #region Private Fields

        // Components
        private MoveComponent _moveComponent;
        private RotationComponent _rotationComponent;
        private JumpComponent _jumpComponent;
        private WeaponsController _weaponController;
        private AnimationsController _animationsController;
        private CameraController _cameraController;
        private AudioController _audioController;

        #endregion

        #region Events

        public event Action<bool, float> OnMove;
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
            CheckMovementInput();
            CheckRotationInput();
            CheckJumpInput();
            CheckWeaponInput();
            CheckLookUpDown();
            CheckAmmoSlider();
        }

        #endregion

        #region Private Methods

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

        private void GetRequiredComponent()
        {
            _weaponController = GetComponent<WeaponsController>();
            _weaponController.SuscribeEvents(this);

            _animationsController = GetComponent<AnimationsController>();
            _animationsController.SuscribeEvents(this);

            OnChangeWeapon?.Invoke(_weaponController.WeaponList[0]);

            _moveComponent = GetComponent<MoveComponent>();
            _rotationComponent = GetComponent<RotationComponent>();
            _jumpComponent = GetComponent<JumpComponent>();

            _cameraController = GetComponent<CameraController>();
            _cameraController.SuscribeEvents(this);

            _audioController = GetComponent<AudioController>();
            _audioController.SuscribeEvents(this);
        }

        private void CheckMovementInput()
        {
            float currentSpeed;

            if (Input.GetKey(_runKey))
            {
                currentSpeed = _moveComponent.RunSpeed;
            }
            else if (Input.GetKey(_sneakKey))
            {
                currentSpeed = _moveComponent.SneakSpeed;
            }
            else
            {
                currentSpeed = _moveComponent.WalkSpeed;
            }

            var xMove = transform.right * Input.GetAxisRaw(_horizontalAxis);
            var yMove = transform.forward * Input.GetAxisRaw(_verticalAxis);

            var direction = xMove + yMove;
            direction.Normalize();

            _moveComponent.DoMove(direction, currentSpeed);

            OnWalk?.Invoke(direction != Vector3.zero && currentSpeed == _moveComponent.WalkSpeed);
            OnRun?.Invoke(direction != Vector3.zero && currentSpeed == _moveComponent.RunSpeed);
            OnSneak?.Invoke(direction != Vector3.zero && currentSpeed == _moveComponent.SneakSpeed);
        }

        private void CheckRotationInput()
        {
            var mouseInput = Input.GetAxisRaw(_rotationAxis) * (_xMouseSensibility * 1000);
            _rotationComponent.Rotate(mouseInput);
        }

        private void CheckJumpInput()
        {
            if (_jumpComponent.CheckIsGrounded() && Input.GetKeyDown(_jumpKey))
            {
                _jumpComponent.DoJump();
            }
        }

        private void CheckWeaponInput()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !_weaponController.CurrentWeapon.Equals(_weaponController.WeaponList[0]))
                OnChangeWeapon?.Invoke(_weaponController.WeaponList[0]);
            if (Input.GetKeyDown(KeyCode.Alpha2) && !_weaponController.CurrentWeapon.Equals(_weaponController.WeaponList[1]))
                OnChangeWeapon?.Invoke(_weaponController.WeaponList[1]);

            if (Input.GetMouseButtonDown((int)_aimMouseButton)) OnAimOn?.Invoke();
            if (Input.GetMouseButtonUp((int)_aimMouseButton)) OnAimOff?.Invoke();
            
            if (Input.GetKeyDown(KeyCode.R)) OnReload?.Invoke(_weaponController.CurrentWeapon);

            if (((IGun)_weaponController.CurrentWeapon).IsAutomatic)
            {
                if (Input.GetMouseButton((int)_attackMouseButton)) OnAttack?.Invoke(_weaponController.CurrentWeapon);
            }
            else
            {
                if (Input.GetMouseButtonDown((int)_attackMouseButton)) OnAttack?.Invoke(_weaponController.CurrentWeapon);
            }

            if (Input.GetKeyDown(_inspectKey)) OnInspect?.Invoke();

            if (Input.GetKeyDown(_holsterKey)) OnHolster?.Invoke();

            if (Input.GetKeyDown(_knifeAttack1Key)) OnKnifeAttack1?.Invoke();
            if (Input.GetKeyDown(_knifeAttack2Key)) OnKnifeAttack2?.Invoke();

            if (Input.GetKeyDown(_granadeKey)) OnThrowGrenade?.Invoke();
        }

        private void CheckLookUpDown()
        {
            var mouseInput = Input.GetAxisRaw(_lookUpDownAxis) * (_yMouseSensibility * 1000);
            _cameraController.LookUpDown(mouseInput);
        }

        #endregion
    }
}
