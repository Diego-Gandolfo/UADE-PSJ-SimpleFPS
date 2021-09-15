namespace SimpleFPS.Projectiles
{
    public interface IBullet
    {
        #region Propertys

        float Damage { get; }

        #endregion

        #region Public Methods

        void SetDamage(float damage);

        #endregion
    }
}
