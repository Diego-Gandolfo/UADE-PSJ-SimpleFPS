namespace Assets._Main.Scripts.Strategy
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
