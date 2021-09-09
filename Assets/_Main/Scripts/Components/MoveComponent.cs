using Assets._Main.Scripts.Strategy;
using UnityEngine;

namespace Assets._Main.Scripts.Component
{
    public class MoveComponent : MonoBehaviour, IMove
    {
        #region Serialize Fields

        [SerializeField] private float _walkSpeed = 7f;
        [SerializeField] private float _runSpeed = 14f;
        [SerializeField] private float _sneakSpeed = 3.5f;

        #endregion

        #region Propertys

        public float WalkSpeed => _walkSpeed;
        public float RunSpeed => _runSpeed;
        public float SneakSpeed => _sneakSpeed;

        #endregion

        #region Public Methods

        public void DoMove(Vector3 direction, float speed)
        {
            transform.position += (direction * (speed * Time.deltaTime));
        }

        #endregion
    }
}
