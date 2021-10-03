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
        [SerializeField] private float _minWaitTime = 0f;
        [SerializeField] private float _maxWaitTime = 0f;
        [SerializeField] private float _minDistance = 0;

        [Header("Patrol Area Settings")]
        [SerializeField] private Transform _patrolCenter = null;
        [SerializeField] private Vector3 _areaSize = Vector3.zero;

        [Header("Raycast Settings")]
        [SerializeField] private float _rayDistance = 0f;
        [SerializeField] private LayerMask _rayLayerMask = 0;
        [SerializeField] private float _rayOffset = 0f;

        #endregion

        #region Private Fields

        private EnemyManager _enemyManager;
        private GameObject _patrolPosition = null;
        private float _waitTimeCounter = 0.0f;
        private float _minX = 0;
        private float _maxX = 0;
        private float _minZ = 0;
        private float _maxZ = 0;
        private bool _canMove = true;
        private bool _canCount = false;

        #endregion


        private void Awake()
        {
            if (_patrolCenter == null) _patrolCenter = transform;
        }

        private void Start()
        {
            _enemyManager = EnemyManager.Instance;

            _patrolPosition = new GameObject("Patrol Position");
            _patrolPosition.SetActive(false);

            _minX = (_areaSize.x / -2) + _patrolCenter.position.x;
            _maxX = (_areaSize.x / 2) + _patrolCenter.position.x;
            _minZ = (_areaSize.z / -2) + _patrolCenter.position.z;
            _maxZ = (_areaSize.z / 2) + _patrolCenter.position.z;

            _patrolPosition.transform.position = new Vector3(Random.Range(_minX, _maxX), transform.position.y, Random.Range(_minZ, _maxZ));
            _waitTimeCounter = Random.Range(_minWaitTime, _maxWaitTime);
        }

        private void Update()
        {

            if (_canMove)
            {
                var xzPatrolPosition = new Vector3(_patrolPosition.transform.position.x, transform.position.y, _patrolPosition.transform.position.z);

                var direction = xzPatrolPosition - transform.position;
                direction.Normalize();

                transform.LookAt(xzPatrolPosition);
                _enemyManager.AddCommand(new CmdMovement(gameObject, direction, _patrolSpeed));

                if (Vector2.Distance(transform.position, _patrolPosition.transform.position) < 0.2f)
                {
                    _patrolPosition.transform.position = new Vector3(Random.Range(_minX, _maxX), transform.position.y, Random.Range(_minZ, _maxZ));

                    while (Vector2.Distance(transform.position, _patrolPosition.transform.position) < _minDistance)
                    {
                        _patrolPosition.transform.position = new Vector3(Random.Range(_minX, _maxX), transform.position.y, Random.Range(_minZ, _maxZ));
                    }

                    _enemyManager.AddCommand(new CmdMovement(gameObject, Vector3.zero, 0f));
                    _waitTimeCounter = Random.Range(_minWaitTime, _maxWaitTime);
                    _canMove = false;
                    _canCount = true;
                }
            }

            if (_waitTimeCounter <= 0 && _canCount)
            {
                _canCount = false;
                _canMove = true;
            }
            else
            {
                _waitTimeCounter -= Time.deltaTime;
            }

}

private void FixedUpdate()
        {
            Vector2 rayPosition = transform.position + (_patrolPosition.transform.position - transform.position).normalized * _rayOffset;
            Vector2 rayDirection = (_patrolPosition.transform.position - transform.position).normalized;

            RaycastHit2D raycast = Physics2D.Raycast(rayPosition, rayDirection, _rayDistance, _rayLayerMask);
            Debug.DrawRay(rayPosition, rayDirection * _rayDistance, Color.blue);

            if (raycast)
                _patrolPosition.transform.position = transform.position;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null) rb.velocity = Vector2.zero;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.9f, 0f, 0f, 0.25f);
            if (_patrolCenter != null) Gizmos.DrawCube(_patrolCenter.position, new Vector3(_areaSize.x + _enemySize.x, 0.01f, _areaSize.z + _enemySize.z));
            else Gizmos.DrawCube(transform.position, new Vector3(_areaSize.x + _enemySize.x, 0.01f, _areaSize.z + _enemySize.z));
        }

        private void OnDisable()
        {
            Destroy(_patrolPosition);
        }
    }
}
