using SimpleFPS.Command;
using SimpleFPS.Enemy;
using SimpleFPS.Movement;
using UnityEngine;

namespace SimpleFPS.Patrol
{
    public class FollowTarget : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private Transform _target = null;
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _stopDistance = 10f;

        #endregion

        #region Private Fields

        private CommandManager _commandManager;
        private Vector3 _direction;

        #endregion

        #region Propertys

        public Vector3 Direction => _direction;

        #endregion

        #region Unity Methods

        void Start()
        {
            if (_target == null) Debug.LogError($"{this} en {this.gameObject} no tiene asignado el Target");
            _commandManager = CommandManager.Instance;
        }

        void Update()
        {
            var xzTargetPosition = new Vector3(_target.position.x, transform.position.y, _target.position.z);

            _direction = xzTargetPosition - transform.position;
            _direction.Normalize();

            transform.LookAt(xzTargetPosition);

            if(Vector3.Distance(transform.position, _target.position) > _stopDistance)
            {
                _commandManager.AddCommand(new CmdMovement(gameObject, _direction, _moveSpeed));
            }
        }

        #endregion
    }
}
