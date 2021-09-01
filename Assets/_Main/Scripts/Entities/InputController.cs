using Assets._Main.Scripts.Controllers;
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
    [RequireComponent(typeof(MoveController))]
    [RequireComponent(typeof(RotationController))]
    [RequireComponent(typeof(JumpController))]
    [RequireComponent(typeof(WeaponsController))]
    [RequireComponent(typeof(AnimationsController))]
    public class InputController : MonoBehaviour, IInputController
    {
        #region Serialize Fields

        [Header("Movement")]
        [SerializeField] private string _horizontalAxis = "Horizontal";
        [SerializeField] private string _verticalAxis = "Vertical";
        [SerializeField] private KeyCode _runKey = KeyCode.LeftShift;

        [Header("Rotation")]
        [SerializeField] private string _rotationAxis = "Mouse X";
        [SerializeField, Range(0, 1)] private float _mouseSensibility = 0.5f;

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

        #endregion

        #region Private Fields

        // Components
        private MoveController _moveController;
        private RotationController _rotationController;
        private JumpController _jumpController;
        private WeaponsController _weaponController;
        private AnimationsController _animationsController;
        //private Animator _animator;

        #endregion

        #region Events

        public event Action OnReload;
        public event Action OnAttack;
        public event Action OnInspect;
        public event Action OnHolster;
        public event Action OnKnifeAttack1;
        public event Action OnKnifeAttack2;
        public event Action OnThrowGrenade;
        public event Action<bool> OnAim;
        public event Action<IWeapon> OnChangeWeapon;
        public event Action<bool> OnWalk;
        public event Action<bool> OnRun;

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
        }

        #endregion

        #region Private Methods

        private void GetRequiredComponent()
        {
            _weaponController = GetComponent<WeaponsController>();
            _weaponController.SuscribeEvents(this);

            _animationsController = GetComponent<AnimationsController>();
            _animationsController.SuscribeEvents(this);

            OnChangeWeapon?.Invoke(_weaponController.WeaponList[0]);

            _moveController = GetComponent<MoveController>();
            _rotationController = GetComponent<RotationController>();
            _jumpController = GetComponent<JumpController>();
        }

        private void CheckMovementInput()
        {
            var currentSpeed = Input.GetKey(_runKey) ? _moveController.RunSpeed : _moveController.WalkSpeed;

            var xMove = transform.right * Input.GetAxisRaw(_horizontalAxis);
            var yMove = transform.forward * Input.GetAxisRaw(_verticalAxis);

            var direction = xMove + yMove;
            direction.Normalize();

            _moveController.Move(direction, currentSpeed);

            OnWalk?.Invoke(direction != Vector3.zero && currentSpeed == _moveController.WalkSpeed);
            OnRun?.Invoke(direction != Vector3.zero && currentSpeed == _moveController.RunSpeed);
        }

        private void CheckRotationInput()
        {
            var mouseInput = Input.GetAxisRaw(_rotationAxis) * (_mouseSensibility * 1000);
            _rotationController.Rotate(mouseInput);
        }

        private void CheckJumpInput()
        {
            if (_jumpController.CheckIsGrounded() && Input.GetKeyDown(_jumpKey))
            {
                _jumpController.Jump();
            }
        }

        private void CheckWeaponInput()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) OnChangeWeapon?.Invoke(_weaponController.WeaponList[0]);
            if (Input.GetKeyDown(KeyCode.Alpha2)) OnChangeWeapon?.Invoke(_weaponController.WeaponList[1]);

            OnAim?.Invoke(Input.GetMouseButton((int)_aimMouseButton));
            
            if (Input.GetKeyDown(KeyCode.R)) OnReload?.Invoke();
            if (Input.GetMouseButtonDown((int)_attackMouseButton)) OnAttack?.Invoke();

            if (Input.GetKeyDown(_inspectKey)) OnInspect?.Invoke();

            if (Input.GetKeyDown(_holsterKey)) OnHolster?.Invoke();

            if (Input.GetKeyDown(_knifeAttack1Key)) OnKnifeAttack1?.Invoke();
            if (Input.GetKeyDown(_knifeAttack2Key)) OnKnifeAttack2?.Invoke();

            if (Input.GetKeyDown(_granadeKey)) OnThrowGrenade?.Invoke();
        }

        #endregion
    }
}
