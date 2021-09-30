using SimpleFPS.FPS;
using UnityEngine;

public enum MouseButton
{
    Left,
    Right,
    Middle
}

namespace SimpleFPS.Player
{
    public class InputController : MonoBehaviour
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
        private FPSCharacterController _characterController;

        #endregion

        #region Unity Methods

        private void Start()
        {
            GetRequiredComponent();
        }

        private void Update()
        {
            CheckMovementInput();
            CheckRotationInput();
            CheckLookUpDown();
            CheckJumpInput();
            CheckWeaponChangeInput();

            CheckWeaponInput();
        }

        #endregion

        #region Private Methods

        private void GetRequiredComponent()
        {
            _characterController = GetComponent<FPSCharacterController>();
        }

        private void CheckMovementInput()
        {
            var xMove = transform.right * Input.GetAxisRaw(_horizontalAxis);
            var yMove = transform.forward * Input.GetAxisRaw(_verticalAxis);

            var direction = xMove + yMove;
            direction.Normalize();

            if (Input.GetKey(_runKey))
            {
                _characterController.DoMovement("Run", direction);
            }
            else if (Input.GetKey(_sneakKey))
            {
                _characterController.DoMovement("Sneak", direction);
            }
            else
            {
                _characterController.DoMovement("Walk", direction);
            }
        }

        private void CheckRotationInput()
        {
            var mouseInput = Input.GetAxisRaw(_rotationAxis) * (_xMouseSensibility * 1000);
            _characterController.DoRotation(mouseInput);
        }

        private void CheckLookUpDown()
        {
            var mouseInput = Input.GetAxisRaw(_lookUpDownAxis) * (_yMouseSensibility * 1000);
            _characterController.DoLookUpDown(mouseInput);
        }

        private void CheckJumpInput()
        {
            if (Input.GetKeyDown(_jumpKey))
            {
                _characterController.DoJump();
            }
        }

        private void CheckWeaponChangeInput()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _characterController.DoWeaponChange(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _characterController.DoWeaponChange(1);
            }
        }

        private void CheckWeaponInput()
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
