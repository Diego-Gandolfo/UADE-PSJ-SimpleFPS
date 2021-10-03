using SimpleFPS.Enemy;
using UnityEngine;

namespace SimpleFPS.Patrol
{
    public class RobotScout : MonoBehaviour
    {
        #region Serialize Fields

        [Header("Patrol Settings")]
        [SerializeField] private DetectTarget _visionCone;

        [Header("Health")]
        [SerializeField] private ParticleSystem _damageParticles1;
        [SerializeField] private ParticleSystem _damageParticles2;
        [SerializeField] private ParticleSystem _damageParticles3;

        #endregion

        #region Private Fields

        // Componentes
        private PatrolArea _patrolArea;
        private FollowTarget _followTarget;
        private ExploteOnCollision _exploteOnCollision;
        private MeshRenderer _meshRenderer;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _patrolArea = GetComponent<PatrolArea>();
            _followTarget = GetComponent<FollowTarget>();
            _meshRenderer = GetComponentInChildren<MeshRenderer>();

            _exploteOnCollision = GetComponent<ExploteOnCollision>();
            if (_exploteOnCollision == null) Debug.LogError($"{this.gameObject.name} no tiene un ExploteOnCollision");
            else _exploteOnCollision.OnExplotion += OnExplotionHandler;

            if (_visionCone == null) Debug.LogError($"{this.gameObject.name} no tiene asignado un DetectTarget");
            else _visionCone.OnDetection += OnDetectionHandler;

            _patrolArea.enabled = true;
            _visionCone.gameObject.SetActive(true);
            _followTarget.enabled = false;
        }

        #endregion

        #region Private Methods

        private void OnDetectionHandler()
        {
            _patrolArea.enabled = false;
            _visionCone.gameObject.SetActive(false);
            _followTarget.enabled = true;
        }

        private void OnExplotionHandler()
        {
            _patrolArea.enabled = false;
            _visionCone.gameObject.SetActive(false);
            _followTarget.enabled = false;
            _meshRenderer.enabled = false;
        }

        #endregion
    }
}
