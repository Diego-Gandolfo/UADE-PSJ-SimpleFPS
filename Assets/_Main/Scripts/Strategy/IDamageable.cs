using System;

namespace SimpleFPS.Life
{
    public interface IDamageable
    {
        #region Events

        event Action OnRecieveDamage;

        #endregion

        #region Public Methods

        void ReceiveDamage(float damage);

        #endregion
    }
}
