﻿using Assets._Main.Scripts.Flyweight.ScriptableObjects;
using System;

namespace Assets._Main.Scripts.Strategy
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
