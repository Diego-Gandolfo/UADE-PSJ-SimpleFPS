using SimpleFPS.Command;
using SimpleFPS.Life;
using UnityEngine;

namespace SimpleFPS.Enemy.Mine
{
    public class Mine : MonoBehaviour
    {
        #region Private Fields

        // Componentes
        private Health _healthComponent;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _healthComponent = GetComponent<Health>();
            if (_healthComponent == null) Debug.LogError($"{this.gameObject.name} no tiene asignado un HealthComponent");
            else
            {
                _healthComponent.OnDie += OnDieHandler;
            }
        }

        #endregion

        #region Private Methods

        private void OnDieHandler()
        {
            EnemyManager.Instance.AddCommand(new CmdExplosion(transform.position, transform.rotation));
        }

        #endregion
    }
}
