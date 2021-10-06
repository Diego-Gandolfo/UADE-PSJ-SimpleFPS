using SimpleFPS.Generics.Pool;
using SimpleFPS.Managers;
using SimpleFPS.Projectiles;
using UnityEngine;

namespace SimpleFPS.Weapons
{
    public class AsaultRifle : BaseGun
    {
        #region Private Fields

        private GameManager _gameManager;
        private float _cooldownTimer;

        #endregion

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();
            _canAttack = true;
        }

        protected override void Start()
        {
            base.Start();
            _gameManager = GameManager.Instance;
        }

        private void Update()
        {
            if (!_gameManager.IsPaused)
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
        }

        #endregion

        #region Public Methods

        public override void Attack()
        {
            _canAttack = false;

            base.Attack();

            _cooldownTimer = FireCooldown;
        }

        public override void Reload()
        {
            base.Reload();
        }

        #endregion
    }
}
