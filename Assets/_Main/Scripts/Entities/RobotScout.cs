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

        #endregion

        #region Unity Methods

        private void Start()
        {
            _patrolArea = GetComponent<PatrolArea>();
            _followTarget = GetComponent<FollowTarget>();

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

        #endregion
    }
}
