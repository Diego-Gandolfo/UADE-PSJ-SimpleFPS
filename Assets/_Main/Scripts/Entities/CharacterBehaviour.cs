using Assets._Main.Scripts.Controllers;
using UnityEngine;

public enum MouseButton
{
    Left,
    Right,
    Middle
}

namespace Assets._Main.Scripts.Entities
{
    [RequireComponent(typeof(MoveController))]
    [RequireComponent(typeof(RotationController))]
    [RequireComponent(typeof(JumpController))]
    [RequireComponent(typeof(WeaponsController))]
    public class CharacterBehaviour : MonoBehaviour
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
        [SerializeField] private MouseButton _shootMouseButton = MouseButton.Left;
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
        private Animator _animator;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            GetRequiredComponent();

            ChangeWeapon(0);
        }

        private void Update()
        {
            CheckMovementInput();
            CheckRotationInput();
            CheckJumpInput();
            CheckWeaponInput();

            CheckCurrentAmmo();
        }

        #endregion

        #region Private Methods

        private void GetRequiredComponent()
        {
            _moveController = GetComponent<MoveController>();
            _rotationController = GetComponent<RotationController>();
            _jumpController = GetComponent<JumpController>();
            _weaponController = GetComponent<WeaponsController>();
        }

        private void CheckMovementInput()
        {
            var currentSpeed = Input.GetKey(_runKey) ? _moveController.RunSpeed : _moveController.WalkSpeed;

            var xMove = transform.right * Input.GetAxisRaw(_horizontalAxis);
            var yMove = transform.forward * Input.GetAxisRaw(_verticalAxis);

            var direction = xMove + yMove;
            direction.Normalize();

            _moveController.Move(direction, currentSpeed);

            _animator.SetBool("Walk", direction != Vector3.zero && currentSpeed == _moveController.WalkSpeed);
            _animator.SetBool("Run", direction != Vector3.zero && currentSpeed == _moveController.RunSpeed);
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
            if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeWeapon(1);

            if (!_animator.GetBool("Run"))
            {
                _animator.SetBool("Aim", Input.GetMouseButton((int)_aimMouseButton));
            }
            
            if (!_weaponController.IsOutOfAmmo())
            {
                if (Input.GetKeyDown(KeyCode.R)) Reload();
                if (Input.GetMouseButtonDown((int)_shootMouseButton)) Shoot();
            }

            if (Input.GetKeyDown(_inspectKey)) _animator.SetTrigger("Inspect");

            if (Input.GetKeyDown(_holsterKey)) _animator.SetBool("Holster", !_animator.GetBool("Holster"));

            if (Input.GetKeyDown(_knifeAttack1Key)) KnifeAttack1();
            if (Input.GetKeyDown(_knifeAttack2Key)) KnifeAttack2();

            if (Input.GetKeyDown(_granadeKey)) GrenadeThrow();
        }

        private void ChangeWeapon(int index)
        {
            _weaponController.ChangeWeapon(index);
            _animator = _weaponController.Animator;
        }

        private void CheckCurrentAmmo()
        {
            if (_weaponController.CurrentWeapon is HandgunController)
                _animator.SetBool("Out Of Ammo Slider", _weaponController.IsMagazineEmpty());
        }

        private void Reload()
        {
            if (_weaponController.IsMagazineEmpty()) _animator.Play("Reload Out Of Ammo", 0, 0f);
            else _animator.Play("Reload Ammo Left", 0, 0f);

            _weaponController.Reload();
        }

        private void Shoot()
        {
            if (_animator.GetBool("Aim")) _animator.Play("Aim Fire", 0, 0f);
            else _animator.Play("Fire", 0, 0f);

            _weaponController.Attack();
        }

        private void KnifeAttack1()
        {
            _animator.Play("Knife Attack 1", 0, 0f);
        }

        private void KnifeAttack2()
        {
            _animator.Play("Knife Attack 2", 0, 0f);
        }

        private void GrenadeThrow()
        {
            _animator.Play("GrenadeThrow", 0, 0.0f);
        }

        #endregion
    }
}
