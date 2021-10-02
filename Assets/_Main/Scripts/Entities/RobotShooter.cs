using SimpleFPS.Movement;
using SimpleFPS.Player;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Enemy
{
    [RequireComponent(typeof(MoveComponent))]
    public class RobotShooter : MonoBehaviour
    {
        [SerializeField] private EnemyTargetStats _objectiveTargetStats;
        [SerializeField] private EnemyTargetStats _characterTargetStats;

        #region Private Fields

        // Components
        private MoveComponent _moveComponent;

        // Character
        private FPSInputController _character;

        // Objetives
        private List<Transform> _objectivesList = new List<Transform>();
        private Transform _currentTarget;

        #endregion

        #region Unity Methods

        private void Start()
        {
            GetRequiredComponents();
            _character = Managers.LevelManager.Instance.Character;
            _objectivesList = Managers.LevelManager.Instance.ObjectivesList;            
        }

        private void Update()
        {
            SetNearestTarget();
            LookAtTarget();
            MoveToTarget();
        }

        #endregion

        #region Private Methods

        private void GetRequiredComponents()
        {
            _moveComponent = GetComponent<MoveComponent>();
        }

        private void SetNearestTarget()
        {
            var distanceWithPlayer = Vector3.Distance(transform.position, _character.transform.position);
            var isPlayerNear = distanceWithPlayer < _characterTargetStats.MaxDistance;

            if (isPlayerNear)
            {
                _currentTarget = _character.transform;
            }
            else
            {
                var maxDistance = Mathf.Infinity;

                foreach (var objective in _objectivesList)
                {
                    var currentDistance = Vector3.Distance(transform.position, objective.position);

                    if (currentDistance < maxDistance)
                    {
                        maxDistance = currentDistance;
                        _currentTarget = objective;
                    }
                }
            }
        }

        private void MoveToTarget()
        {
            var currentSpeed = 0f;

            var distance = Vector3.Distance(transform.position, _currentTarget.position);

            var direction = _currentTarget.position - transform.position;
            direction = new Vector3(direction.x, 0f, direction.z);
            direction.Normalize();

            if (distance > _objectiveTargetStats.MinDistance && distance < _objectiveTargetStats.MaxDistance)
            {
                currentSpeed = _moveComponent.WalkSpeed;
            }
            else if (distance < _objectiveTargetStats.RetreatDistance)
            {
                currentSpeed = -_moveComponent.WalkSpeed;
            }

            _moveComponent.DoMove(direction, currentSpeed);
        }

        private void LookAtTarget()
        {
            var lookAtPosition = new Vector3(_currentTarget.position.x, transform.position.y, _currentTarget.position.z);
            transform.LookAt(lookAtPosition);
        }

        #endregion
    }
}
