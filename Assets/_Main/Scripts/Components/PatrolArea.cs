using SimpleFPS.Command;
using SimpleFPS.Enemy;
using UnityEngine;

namespace SimpleFPS.Patrol
{
    public class PatrolArea : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Enemy Settings")]
        [SerializeField] private Vector3 _enemySize = Vector3.zero;

        [Header("Patrol Settings")]
        [SerializeField] private float _patrolSpeed = .5f;
        [SerializeField] private float _rotationSpeed = .5f;
        [SerializeField] private float _minWaitTime = 0f;
        [SerializeField] private float _maxWaitTime = 0f;
        [SerializeField] private float _minDistance = 0;

        [Header("Patrol Area Settings")]
        [SerializeField] private Transform _patrolCenter = null;
        [SerializeField] private Vector3 _areaSize = Vector3.zero;

        [Header("Collision Stop Settings")]
        [SerializeField] private float _collisionStopDistance = 0f;
        [SerializeField] private LayerMask _collisionStopLayerMask = 0;

        #endregion

        #region Private Fields

        // Components
        private CommandManager _commandManager;
        private GameObject _patrolPosition = null;

        // Area
        private float _minX = 0;
        private float _maxX = 0;
        private float _minZ = 0;
        private float _maxZ = 0;

        // Flags
        private bool _canRotate = true;
        private bool _canMove = false;
        private bool _canCount = false;

        // Parameters
        private float _waitTimeCounter = 0.0f;
        private float _currentSpeed;
        private Vector3 _direction;

        #endregion

        #region Propertys

        public float CurrentSpeed => _currentSpeed;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (_patrolCenter == null) _patrolCenter = transform;
        }

        private void Start()
        {
            _commandManager = CommandManager.Instance;

            _patrolPosition = new GameObject("Patrol Position");
            _patrolPosition.transform.parent = gameObject.transform.parent;
            _patrolPosition.SetActive(false);
            RandomMovePatrolPosition();

            _minX = (_areaSize.x / -2) + _patrolCenter.position.x;
            _maxX = (_areaSize.x / 2) + _patrolCenter.position.x;
            _minZ = (_areaSize.z / -2) + _patrolCenter.position.z;
            _maxZ = (_areaSize.z / 2) + _patrolCenter.position.z;

            _waitTimeCounter = Random.Range(_minWaitTime, _maxWaitTime);

            _currentSpeed = 0f;
        }

        private void Update()
        {
            if (_canCount)
            {
                if (_waitTimeCounter <= 0)
                {
                    _canCount = false;

                    _waitTimeCounter = Random.Range(_minWaitTime, _maxWaitTime);
                    
                    _canRotate = true;
                }
                else
                {
                    _waitTimeCounter -= Time.deltaTime;
                }
            }

            if (_canRotate)
            {
                var lookRotation = Quaternion.LookRotation(_direction);
                _commandManager.AddCommand(new CmdRotation(transform, lookRotation, _rotationSpeed));


                if (Quaternion.Angle(transform.rotation, lookRotation) < .1f)
                {
                    _canRotate = false;
                    _canMove = true;
                }
            }

            if (_canMove)
            {
                _currentSpeed = _patrolSpeed;

                if (Vector2.Distance(transform.position, _patrolPosition.transform.position) < 0.2f)
                {
                    _canMove = false;

                    _currentSpeed = 0f;
                    RandomMovePatrolPosition();

                    _canCount = true;
                }
            }
        }

        private void FixedUpdate()
        {
            _commandManager.AddCommand(new CmdMovement(gameObject, _direction, _currentSpeed));

            var hits = Physics.OverlapSphere(transform.position, _collisionStopDistance, _collisionStopLayerMask);

            foreach (var hit in hits)
            {
                if (hit.gameObject != gameObject)
                {
                    _canMove = false;
                    _currentSpeed = _patrolSpeed;
                    RandomMovePatrolPosition();
                    _canRotate = true;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.9f, 0f, 0f, 0.25f);
            if (_patrolCenter != null) Gizmos.DrawCube(_patrolCenter.position, new Vector3(_areaSize.x + _enemySize.x, 0.01f, _areaSize.z + _enemySize.z));
            else Gizmos.DrawCube(transform.position, new Vector3(_areaSize.x + _enemySize.x, 0.01f, _areaSize.z + _enemySize.z));

            Gizmos.color = new Color(0f, 0.25f, 0f, 0.25f);
            Gizmos.DrawSphere(transform.position, _collisionStopDistance);
        }

        private void OnDestroy()
        {
            Destroy(_patrolPosition);
        }

        #endregion

        #region Private Methods

        private void RandomMovePatrolPosition()
        {
            _patrolPosition.transform.position = new Vector3(Random.Range(_minX, _maxX), transform.position.y, Random.Range(_minZ, _maxZ));

            while (Vector2.Distance(transform.position, _patrolPosition.transform.position) < _minDistance)
            {
                _patrolPosition.transform.position = new Vector3(Random.Range(_minX, _maxX), transform.position.y, Random.Range(_minZ, _maxZ));
            }

            var xzPatrolPosition = new Vector3(_patrolPosition.transform.position.x, transform.position.y, _patrolPosition.transform.position.z);
            _direction = xzPatrolPosition - transform.position;
            _direction.Normalize();
        }

        #endregion
    }
}
