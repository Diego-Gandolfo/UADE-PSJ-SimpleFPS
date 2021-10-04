using SimpleFPS.FPS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFPS.Enemy
{
    public class Turret : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _detectionRadius;
        [SerializeField] private float _shootingRadius;
        [SerializeField] private Transform _gameObjectToRotate;
        [SerializeField] private float _shootingCooldown;

        #endregion

        #region Private Fields

        private Transform _character;
        private Vector3 _direction;
        private bool _canRotate;
        private bool _canShoot;
        private float _shootingTimer;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _character = Managers.LevelManager.Instance.Character.transform;
            _shootingTimer = _shootingCooldown;
        }

        private void Update()
        {
            if (_canRotate)
            {
                var xzCharacterPosition = new Vector3(_character.position.x, _gameObjectToRotate.position.y, _character.position.z);
                _direction = xzCharacterPosition - _gameObjectToRotate.position;
                _direction.Normalize(); var lookRotation = Quaternion.LookRotation(_direction);
                _gameObjectToRotate.rotation = Quaternion.RotateTowards(_gameObjectToRotate.rotation, lookRotation, .25f);
            }

            if(_canShoot)
            {
                if (_shootingTimer <= 0f)
                {
                    print($"POW!");
                    _shootingTimer = _shootingCooldown;
                }
                else
                {
                    _shootingTimer -= Time.deltaTime;
                }
            }
        }

        private void FixedUpdate()
        {
            var targetsDetected = Physics.OverlapSphere(transform.position, _detectionRadius, _layerMask);

            if (targetsDetected.Length > 0)
            {
                _canRotate = true;
            }
            else
            {
                _canRotate = false;
            }

            var targetsInRange = Physics.OverlapSphere(transform.position, _shootingRadius, _layerMask);

            if (targetsInRange.Length > 0)
            {
                _canShoot = true;
            }
            else
            {
                _canShoot = false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 0.5f, 0f, 0.25f);
            Gizmos.DrawSphere(transform.position, _detectionRadius);

            Gizmos.color = new Color(0.5f, 0f, 0f, 0.25f);
            Gizmos.DrawSphere(transform.position, _shootingRadius);
        }

        #endregion
    }
}
