using Assets._Main.Scripts.Component;
using Assets._Main.Scripts.Flyweight.ScriptableObjects;
using Assets._Main.Scripts.Strategy;
using UnityEngine;

namespace Assets._Main.Scripts.Components
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(AutoDisabler))]
    public class MeleeAttack : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private BaseWeaponStats _baseWeaponStats;

        #endregion

        #region Private Fields

        private float _damage;
        private AutoDisabler _autoDisabler;

        #endregion

        #region Propertys

        public AutoDisabler AutoDisabler => _autoDisabler;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _damage = _baseWeaponStats.Damage;
            GetComponent<Collider>().isTrigger = true;
            _autoDisabler = GetComponent<AutoDisabler>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var healthComponent = other.gameObject.GetComponent<HealthComponent>();

            if (healthComponent != null)
            {
                healthComponent.ReceiveDamage(_damage);
            }
        }

        #endregion
    }
}