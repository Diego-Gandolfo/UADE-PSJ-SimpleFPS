using SimpleFPS.Command;
using UnityEngine;

namespace SimpleFPS.Movement
{
    public class MoveComponent : MonoBehaviour, IMove
    {
        #region Serialize Fields

        [SerializeField] private float _walkSpeed = 7f;
        [SerializeField] private float _runSpeed = 14f;
        [SerializeField] private float _sneakSpeed = 3.5f;

        #endregion

        #region Private Fields

        private CommandManager _commandManager;

        #endregion

        #region Propertys

        public float WalkSpeed => _walkSpeed;
        public float RunSpeed => _runSpeed;
        public float SneakSpeed => _sneakSpeed;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _commandManager = CommandManager.Instance;
        }

        #endregion

        #region Public Methods

        public void DoMove(Vector3 direction, float speed)
        {
            _commandManager.AddCommand(new CmdMovement(gameObject, direction, speed));
        }

        #endregion
    }
}
