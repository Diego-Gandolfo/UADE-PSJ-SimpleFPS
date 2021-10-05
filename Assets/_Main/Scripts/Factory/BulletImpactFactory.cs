using SimpleFPS.Projectiles;

namespace SimpleFPS.Factory
{
    public class BulletImpactFactory : AbstractFactory<BulletImpact>
    {
        #region Constructor

        public BulletImpactFactory(BulletImpact prefab) : base(prefab) { }

        #endregion

        #region Public Methods

        public BulletImpact GetBulletImpact()
        {
            var bulletImpact = Pool.GetInstance();
            return bulletImpact;
        }

        public void StoreBulletImpact(BulletImpact bulletImpact)
        {
            Pool.StoreInstance(bulletImpact);
        }

        #endregion
    }
}
