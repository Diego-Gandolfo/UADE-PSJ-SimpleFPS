using UnityEngine;

namespace SimpleFPS.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveComponent : MonoBehaviour, IMove // TODO: Hacer que el Move solo use una velocidad y que hayan otros Componentes que den la posibilidad de correr o Sneak
    {
        #region Serialize Fields

        [SerializeField] private float _walkSpeed = 7f;
        [SerializeField] private float _runSpeed = 14f;
        [SerializeField] private float _sneakSpeed = 3.5f;

        #endregion

        #region Private Fields

        private Rigidbody _rigidbody;

        #endregion

        #region Propertys

        public float WalkSpeed => _walkSpeed;
        public float RunSpeed => _runSpeed;
        public float SneakSpeed => _sneakSpeed;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        #endregion

        #region Public Methods

        public void DoMove(Vector3 direction, float speed)
        {
            _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
            _rigidbody.AddForce(direction * speed, ForceMode.VelocityChange);
        }

        #endregion
    }
}
