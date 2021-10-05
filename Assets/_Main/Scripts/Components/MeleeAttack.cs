using SimpleFPS.Components;
using SimpleFPS.Life;
using UnityEngine;

namespace SimpleFPS.Weapons
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
            var healthComponent = other.gameObject.GetComponent<Health>();

            if (healthComponent != null)
            {
                healthComponent.ReceiveDamage(_damage);
            }
        }

        #endregion
    }
}