using SimpleFPS.Command;
using SimpleFPS.Factory;
using SimpleFPS.Generics.Pool;
using SimpleFPS.Projectiles;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleFPS.Weapons
{
    public abstract class BaseGun : BaseWeapon, IGun
    {
        #region Serialize Fields

        [Header("Stats")]
        [SerializeField] protected BaseGunStats _baseGunStats;
        [SerializeField] private BulletStats _bulletStats;

        [Header("UI")]
        [SerializeField] protected Text _extraAmmoText;
        [SerializeField] protected Text _magazineAmmoText;
        [SerializeField] private string _weaponName = "WeaponName";
        [SerializeField] protected Text _weaponNameText;
        [SerializeField] private Sprite _weaponIcon;
        [SerializeField] private Image _weaponImage;

        [Header("Spawnpoints")]
        [SerializeField] protected Transform _bulletSpawnpoint;

        [Header("Sparks")]
        [SerializeField] private ParticleSystem _sparkParticles;
        [SerializeField] private int _minSparks = 1;
        [SerializeField] private int _maxSparks = 7;

        [Header("Muzzle Flash")]
        [SerializeField] private ParticleSystem _muzzleFlashParticles;
        [SerializeField] protected Light _muzzleFlashLight;

        #endregion

        #region Protected Fields

        // Components
        protected CommandManager _commandManager;

        // Parameters
        protected int _currentExtraAmmo;
        protected int _currentMagazineAmmo;
        protected const float BULLET_FORCE = 400f;
        protected bool _canAttack = true;

        #endregion

        #region Propertys

        // Ammo
        public int MaxExtraAmmo => _baseGunStats.MaxExtraAmmo;
        public int CurrentExtraAmmo => _currentExtraAmmo;
        public int MaxMagazineAmmo => _baseGunStats.MaxMagazineAmmo;
        public int CurrentMagazineAmmo => _currentMagazineAmmo;
        public bool IsMagazineEmpty => (CurrentMagazineAmmo <= 0);
        public bool IsOutOfAmmo => ((CurrentExtraAmmo + CurrentMagazineAmmo) <= 0);

        // Fire
        public bool IsAutomatic => _baseGunStats.IsAutomatic;
        public float FireCooldown => _baseGunStats.FireCooldown;
        public bool CanAttack => _canAttack;

        #endregion

        #region Unity Methods

        protected virtual void OnEnable()
        {
            _extraAmmoText.text = _currentExtraAmmo.ToString();
            _magazineAmmoText.text = _currentMagazineAmmo.ToString();
            _weaponNameText.text = _weaponName;
            _weaponImage.sprite = _weaponIcon;
        }

        private void Start()
        {
            _commandManager = CommandManager.Instance;

            _currentExtraAmmo = MaxExtraAmmo;
            _currentMagazineAmmo = MaxMagazineAmmo;
            _extraAmmoText.text = _currentExtraAmmo.ToString();
            _magazineAmmoText.text = _currentMagazineAmmo.ToString();
        }

        #endregion

        #region Protected Methods

        protected void TurnMuzzleFlashLightOff()
        {
            _muzzleFlashLight.enabled = false;
        }

        protected void PlayMuzzleFlashParticles()
        {
            _muzzleFlashParticles.Emit(3);
        }

        protected void PlaySparkParticles()
        {
            _sparkParticles.Emit(Random.Range(_minSparks, _maxSparks));
        }

        #endregion

        #region Public Methods

        public override void Attack()
        {
            _commandManager.AddCommand(new CmdShoot(_bulletSpawnpoint, _bulletStats, Damage, BULLET_FORCE));

            _currentMagazineAmmo--;
            _magazineAmmoText.text = _currentMagazineAmmo.ToString();

            _muzzleFlashLight.enabled = true;
            Invoke("TurnMuzzleFlashLightOff", 0.02f);
            PlayMuzzleFlashParticles();
            PlaySparkParticles();
        }

        public virtual void Reload()
        {
            if (_currentExtraAmmo > 0)
            {
                if (_currentExtraAmmo > MaxMagazineAmmo)
                {
                    _currentExtraAmmo -= (MaxMagazineAmmo - _currentMagazineAmmo);
                    _extraAmmoText.text = _currentExtraAmmo.ToString();
                    _currentMagazineAmmo = MaxMagazineAmmo;
                    _magazineAmmoText.text = _currentMagazineAmmo.ToString();
                }
                else
                {
                    _currentMagazineAmmo = _currentExtraAmmo;
                    _magazineAmmoText.text = _currentMagazineAmmo.ToString();
                    _currentExtraAmmo = 0;
                    _extraAmmoText.text = _currentExtraAmmo.ToString();
                }
            }
        }

        #endregion
    }
}
