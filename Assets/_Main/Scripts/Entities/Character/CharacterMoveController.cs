using UnityEngine;

namespace Assets._Main.Scripts.Entities.Character
{
    public class CharacterMoveController : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _runSpeed;

        #endregion

        #region Unity Methods

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.position += (transform.right * (Input.GetAxisRaw("Horizontal") * _runSpeed * Time.deltaTime));
                transform.position += (transform.forward * (Input.GetAxisRaw("Vertical") * _runSpeed * Time.deltaTime));
            }
            else
            {
                transform.position += (transform.right * (Input.GetAxisRaw("Horizontal") * _moveSpeed * Time.deltaTime));
                transform.position += (transform.forward * (Input.GetAxisRaw("Vertical") * _moveSpeed * Time.deltaTime));
            }
        }

        #endregion
    }
}
