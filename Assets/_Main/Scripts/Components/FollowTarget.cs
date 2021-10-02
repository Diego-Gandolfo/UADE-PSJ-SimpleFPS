using SimpleFPS.Movement;
using UnityEngine;

namespace SimpleFPS.Patrol
{
    [RequireComponent(typeof(MoveComponent))]
    public class FollowTarget : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private Transform _target;

        #endregion

        #region Private Fields

        private MoveComponent _moveComponent;

        #endregion

        #region Unity Methods

        void Start()
        {
            if (_target == null) Debug.LogError($"{this} en {this.gameObject} no tiene asignado el Target");
            _moveComponent = GetComponent<MoveComponent>();
        }

        void Update()
        {
            var xzTranformPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            var xzTargetPosition = new Vector3(_target.position.x, transform.position.y, _target.position.z);

            var direction = xzTargetPosition - xzTranformPosition;
            direction.Normalize();

            transform.LookAt(xzTargetPosition);
            _moveComponent.DoMove(direction, _moveComponent.WalkSpeed);
        }

        #endregion
    }
}
