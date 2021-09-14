namespace SimpleFPS.Weapons
{
    public interface IWeapon
    {
        #region Propertys

        float Damage { get; }

        #endregion

        #region Public Methods

        void Attack();

        #endregion
    }
}
