using SimpleFPS.Projectiles;

namespace SimpleFPS.Factory
{
    public class BulletFactory : AbstractFactory<Bullet>
    {
        #region Constructor

        public BulletFactory(Bullet prefab) : base (prefab) { }

        #endregion

        #region Public Methods

        public Bullet GetBullet(BulletStats stats)
        {
            var bullet = Pool.GetInstance();
            bullet.SetStats(stats);
            return bullet;
        }

        public void StoreBullet(Bullet bullet)
        {
            Pool.StoreInstance(bullet);
        }

        #endregion
    }
}
