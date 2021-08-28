using Assets._Main.Scripts.Controllers;
using UnityEngine;

namespace Assets._Main.Scripts.Entities
{
    [RequireComponent(typeof(MoveController))]
    [RequireComponent(typeof(RotationController))]
    [RequireComponent(typeof(JumpController))]
    [RequireComponent(typeof(AnimationController))]
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

        #endregion

        #region Private Fields

        // Components
        private MoveController _moveController;
        private RotationController _rotationController;
        private JumpController _jumpController;
        private AnimationController _animatorController;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _moveController = GetComponent<MoveController>();
            _rotationController = GetComponent<RotationController>();
            _jumpController = GetComponent<JumpController>();
            _animatorController = GetComponent<AnimationController>();
        }

        private void Update()
        {
            CheckMovementInput();
            CheckRotationInput();
            CheckJumpInput();
            CheckChangeWeaponInput();
        }

        #endregion

        #region Private Methods

        private void CheckMovementInput()
        {
            var currentSpeed = Input.GetKey(_runKey) ? _moveController.RunSpeed : _moveController.WalkSpeed;

            var xMove = transform.right * Input.GetAxisRaw(_horizontalAxis);
            var yMove = transform.forward * Input.GetAxisRaw(_verticalAxis);

            var direction = xMove + yMove;
            direction.Normalize();

            _moveController.Move(direction, currentSpeed);

            _animatorController.SetBool("Walk", direction != Vector3.zero && currentSpeed == _moveController.WalkSpeed);
            _animatorController.SetBool("Run", direction != Vector3.zero && currentSpeed == _moveController.RunSpeed);
        }

        private void CheckRotationInput()
        {
            var mouseInput = _mouseSensibility * Input.GetAxisRaw(_rotationAxis) * 1000;
            _rotationController.Rotate(mouseInput);
        }

        private void CheckJumpInput()
        {
            if (_jumpController.CheckIsGrounded() && Input.GetKeyDown(_jumpKey))
            {
                _jumpController.Jump();
            }
        }

        private void CheckChangeWeaponInput()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) _animatorController.ChangeWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) _animatorController.ChangeWeapon(1);
        }

        #endregion
    }
}
