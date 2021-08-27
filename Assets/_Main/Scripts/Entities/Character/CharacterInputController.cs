using UnityEngine;

namespace Assets._Main.Scripts.Entities.Character
{
    [RequireComponent(typeof(CharacterMoveController))]
    [RequireComponent(typeof(CharacterRotationController))]
    [RequireComponent(typeof(CharacterJumpController))]
    public class CharacterInputController : MonoBehaviour
    {
        #region Serialize Fields

        [Header("Movement")]
        [SerializeField] private string _horizontalAxis = "Horizontal";
        [SerializeField] private string _verticalAxis = "Vertical";
        [SerializeField] private KeyCode _runKey = KeyCode.LeftShift;

        [Header("Rotation")]
        [SerializeField] private string _rotationAxis = "Mouse X";
        [SerializeField, Range(0, 1)] private float _mouseSencibility = 0.5f;

        [Header("Jump")]
        [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

        #endregion

        #region Private Fields

        // Components
        private CharacterMoveController _moveController;
        private CharacterRotationController _rotationController;
        private CharacterJumpController _jumpController;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _moveController = GetComponent<CharacterMoveController>();
            _rotationController = GetComponent<CharacterRotationController>();
            _jumpController = GetComponent<CharacterJumpController>();
        }

        private void Update()
        {
            CheckMovementInput();
            CheckRotationInput();
            CheckJumpInput();
        }

        #endregion

        #region Private Methods

        private void CheckMovementInput()
        {
            var currentSpeed = Input.GetKey(_runKey) ? _moveController.RunSpeed : _moveController.MoveSpeed;

            var xMove = transform.right * Input.GetAxisRaw(_horizontalAxis);
            var yMove = transform.forward * Input.GetAxisRaw(_verticalAxis);

            var direction = xMove + yMove;
            direction.Normalize();

            _moveController.Move(direction, currentSpeed);
        }

        private void CheckRotationInput()
        {
            var mouseInput = _mouseSencibility * Input.GetAxisRaw(_rotationAxis) * 1000;
            _rotationController.Rotate(mouseInput);
        }

        private void CheckJumpInput()
        {
            if (_jumpController.CheckIsGrounded() && Input.GetKeyDown(_jumpKey))
            {
                _jumpController.Jump();
            }
        }

        #endregion
    }
}
