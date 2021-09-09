using Assets._Main.Scripts.Flyweight.ScriptableObjects;
using Assets._Main.Scripts.Generics;
using Assets._Main.Scripts.Strategy;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Main.Scripts.Entities
{
    public class BaseGun : BaseWeapon, IGun
    {
        #region Serialize Fields

        [Header("Stats")]
        [SerializeField] protected BaseGunStats _baseGunStats;

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

        protected int _currentExtraAmmo;
        protected int _currentMagazineAmmo;
        protected const float BULLET_FORCE = 400f;

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

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            _extraAmmoText.text = _currentExtraAmmo.ToString();
            _magazineAmmoText.text = _currentMagazineAmmo.ToString();
            _weaponNameText.text = _weaponName;
            _weaponImage.sprite = _weaponIcon;
        }

        private void Start()
        {
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

        public override void Attack(IWeaponController weaponController) { }

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
