using SimpleFPS.Flyweight.Life;
using System;

namespace SimpleFPS.Strategy.Life
{
    public interface IDamageable
    {
        #region Propertys

        LifeStats LifeStats { get; }
        float MaxLife { get; }
        float CurrentLife { get; }

        #endregion

        #region Events

        event Action OnDie;

        #endregion

        #region Public Methods

        void ReceiveDamage(float damage);
        void ReceiveHeal(float heal);

        #endregion
    }
}
