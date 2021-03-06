using System;
using UnityEngine;

namespace SimpleFPS.Life
{
    public class Health : MonoBehaviour, IAlive, IDamageable, IKilleable, IHealeable
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
        public event Action OnRecieveDamage;

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
            OnRecieveDamage?.Invoke();
            if (_currentLife <= 0f) OnDie?.Invoke();
        }

        public void ReceiveHeal(float heal)
        {
            _currentLife += heal;
            OnRecieveDamage?.Invoke();
            if (_currentLife >= _maxLife) _currentLife = _maxLife;
        }

        #endregion
    }
}
