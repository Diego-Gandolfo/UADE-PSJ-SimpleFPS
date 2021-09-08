using Assets._Main.Scripts.Strategy;
using UnityEngine;

namespace Assets._Main.Scripts.Component
{
    [RequireComponent(typeof(Rigidbody))]
    public class Jump : MonoBehaviour, IJump
    {
        #region Serialize Fields

        [SerializeField] private float _jumpForce = 25f;

        #endregion

        #region Private Fields

        // Components
        private Rigidbody _rigidbody;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        #endregion

        #region Public Methods

        public void DoJump()
        {
            var jumpForce = transform.up * _jumpForce;
            _rigidbody.AddForce(jumpForce, ForceMode.Impulse);
        }

        public bool CheckIsGrounded()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(ray, out hit, 1.1f))
            {
                if (hit.collider != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
