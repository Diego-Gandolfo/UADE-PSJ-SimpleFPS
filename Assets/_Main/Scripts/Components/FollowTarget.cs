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

        #endregion

        #region Private Fields

        //private MoveComponent _moveComponent;

        #endregion

        #region Unity Methods

        void Start()
        {
            if (_target == null) Debug.LogError($"{this} en {this.gameObject} no tiene asignado el Target");
            //_moveComponent = GetComponent<MoveComponent>();
        }

        void Update()
        {
            var xzTargetPosition = new Vector3(_target.position.x, transform.position.y, _target.position.z);

            var direction = xzTargetPosition - transform.position;
            direction.Normalize();

            transform.LookAt(xzTargetPosition);
            //_moveComponent.DoMove(direction, _moveComponent.WalkSpeed);
            EnemyManager.Instance.AddCommand(new CmdMovement(gameObject, direction, _moveSpeed));

        }

        #endregion
    }
}
