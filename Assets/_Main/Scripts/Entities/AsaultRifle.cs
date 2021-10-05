using SimpleFPS.Generics.Pool;
using SimpleFPS.Projectiles;
using UnityEngine;

namespace SimpleFPS.Weapons
{
    public class AsaultRifle : BaseGun
    {
        #region Private Fields

        private float _cooldownTimer;

        #endregion

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();
            _canAttack = true;
        }

        private void Update()
        {
            if (_cooldownTimer <= 0f)
            {
                _canAttack = true;
            }
            else
            {
                _cooldownTimer -= Time.deltaTime;
            }
        }

        #endregion

        #region Public Methods

        public override void Attack()
        {
            _canAttack = false;

            base.Attack();

            _cooldownTimer = _baseGunStats.FireCooldown;
        }

        public override void Reload()
        {
            base.Reload();
        }

        #endregion
    }
}
