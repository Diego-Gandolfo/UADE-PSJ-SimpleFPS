using SimpleFPS.Projectiles;
using UnityEngine;

namespace SimpleFPS.Command
{
    public class CmdShoot : ICommand
    {
        #region Private Fields

        private Transform _spawnpointTranform;
        private BulletStats _bulletStats;
        private float _damage;
        private float _speed;

        #endregion

        #region Constructor

        public CmdShoot(Transform spawnpointTranform, BulletStats bulletStats, float damage, float speed)
        {
            _spawnpointTranform = spawnpointTranform;
            _bulletStats = bulletStats;
            _damage = damage;
            _speed = speed;
        }

        #endregion

        #region Public Methods

        public void Execute()
        {
            if (_spawnpointTranform != null)
            {
                Managers.LevelManager.Instance.BulletFactory.GetBullet(_bulletStats, _spawnpointTranform.position, _spawnpointTranform.rotation, _damage, _speed);
            }
        }

        #endregion
    }
}
