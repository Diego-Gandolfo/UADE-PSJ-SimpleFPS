using Assets._Main.Scripts.Strategy;
using UnityEngine;

namespace Assets._Main.Scripts.Entities
{
    public class Bullet : MonoBehaviour, IBullet
    {
        [SerializeField] private float _timeToDestroy = 1f;

        private IWeaponController _weaponController;
        private float _timer;

        public IWeaponController WeaponController => _weaponController;

        private void OnEnable()
        {
            _timer = _timeToDestroy;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                _weaponController.BulletPool.Store(this);
            }
        }

        public void SetWeaponControlller(IWeaponController weaponController)
        {
            _weaponController = weaponController;
        }
    }
}
