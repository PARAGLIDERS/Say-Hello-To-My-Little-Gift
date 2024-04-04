using Pooling;
using SfxSystem;
using UnityEngine;

namespace GunSystem {
	[CreateAssetMenu(menuName ="Santa/Guns/Gun Config")]
	public class GunConfig : ScriptableObject {
		[SerializeField] private Color _color = Color.white;
		[SerializeField] private bool _isInfinite;
		[SerializeField] private string _name;
		[SerializeField][Range(0f, 1f)] private float _spread = 0.1f;
		[SerializeField][Range(0.1f, 25f)] private float _fireRate = 1f;
		[SerializeField] private int _bulletsPerShot = 1;
		[SerializeField] private BulletType _bulletType = BulletType.Pistol;
		[SerializeField] private SfxShotType _shotSound = SfxShotType.Pistol;
		[SerializeField] private MuzzleFlashType _muzzleFlashType = MuzzleFlashType.Pistol;
		[SerializeField] private InputType _inputType = InputType.Hold;
		[SerializeField] private int _initialAmmo;
		[SerializeField] private int _pickupAmmo;
		[SerializeField] private int _lowAmmo;
		[SerializeField] private int _maxAmmo;

		public Color Color => _color;
		public bool IsInfinite => _isInfinite;
		public string Name => _name;
		public float Spread => _spread;
		public float FireRate => _fireRate;
		public int BulletsPerShot => _bulletsPerShot;
		public BulletType BulletType => _bulletType;
		public SfxShotType ShotSound => _shotSound;
		public MuzzleFlashType MuzzleFlashType => _muzzleFlashType;
		public InputType InputType => _inputType;
		public int InitialAmmo => _initialAmmo;
		public int PickupAmmo => _pickupAmmo;
		public int LowAmmo => _lowAmmo;
		public int MaxAmmo => _maxAmmo;
	}

	public enum BulletType {
		Pistol = PoolType.Bullet_Pistol,
		Uzi = PoolType.Bullet_Uzi,
		Shotgun = PoolType.Bullet_Shotgun,
		Auto = PoolType.Bullet_Auto,
		DoubleShotgun = PoolType.Bullet_DoubleShotgun,
		RocketLauncher = PoolType.Bullet_RocketLauncher,
		Minigun = PoolType.Bullet_Minigun,
	}

	public enum SfxShotType {
		Pistol = SfxType.ShotPistol,
		Uzi = SfxType.ShotUzi,
		Shotgun = SfxType.ShotShotgun,
		Auto = SfxType.ShotAuto,
		DoubleShotgun = SfxType.ShotDoubleShotgun,
		RocketLauncher = SfxType.ShotRocketLauncher,
		Minigun = SfxType.ShotMinigun,
	}

	public enum MuzzleFlashType {
		Pistol = PoolType.MuzzleFlash_Pistol,
		Uzi = PoolType.MuzzleFlash_Uzi, 
		Shotgun = PoolType.MuzzleFlash_Shotgun,
		Auto = PoolType.MuzzleFlash_Auto,
		DoubleShotgun = PoolType.MuzzleFlash_DoubleShotgun, 
		RocketLauncher = PoolType.MuzzleFlash_RocketLauncher, 
		Minigun = PoolType.MuzzleFlash_Minigun,
	}

	public enum InputType {
		Click = 0,
		Hold = 1,
	}
}
