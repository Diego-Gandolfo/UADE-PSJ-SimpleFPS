using System;
using UnityEngine;

namespace SimpleFPS.Damageable
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        #region Serialize Fields

        [SerializeField] private LifeStats _lifeStats;

        #endregion

        #region Private Fields

        private float _maxLife;
        private float _currentLife;

        #endregion

        #region Propertys

        public LifeStats LifeStats => _lifeStats;
        public float MaxLife => _maxLife;
        public float CurrentLife => _currentLife;

        #endregion

        #region Events

        public event Action OnDie;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _maxLife = _lifeStats.MaxLife;
            _currentLife = _maxLife;
        }

        #endregion

        #region Public Methods

        public void ReceiveDamage(float damage)
        {
            _currentLife -= damage;

            if (_currentLife <= 0f) OnDie?.Invoke();
        }

        public void ReceiveHeal(float heal)
        {
            _currentLife += heal;

            if (_currentLife >= _maxLife) _currentLife = _maxLife;
        }

        #endregion
    }
}
