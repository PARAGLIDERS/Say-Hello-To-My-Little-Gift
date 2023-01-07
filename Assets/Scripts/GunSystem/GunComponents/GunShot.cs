using System;
using UnityEngine;

namespace GunSystem.GunComponents {
	public abstract class GunShot {
		public abstract bool Available { get; }
		public abstract void Execute();
	}

	public abstract class GunShotDecorator : GunShot {
		protected readonly GunShot _wrappedShot;
		protected GunShotDecorator(GunShot wrappedShot) {
			_wrappedShot = wrappedShot;
		}
	}

	public class GunShotDefault : GunShot {
		protected readonly Action _shotAction;
		public GunShotDefault(Action shotAction) => _shotAction = shotAction;

		public override bool Available => true;
		public override void Execute() => _shotAction?.Invoke();
	}
	
	public class GunShotAutomatic : GunShotDecorator {
		private readonly int _fireRate;
		private float _timer;

		public GunShotAutomatic(GunShot wrappedShot, int fireRate) : base(wrappedShot) {
			_fireRate = fireRate;
		}

		public override bool Available => Time.time >= _timer;

		public override void Execute() {
			_wrappedShot.Execute();
			_timer = Time.time + 1f / _fireRate;
		}
	}

	public class GunShotShotgun : GunShotDecorator {
		private readonly int _bulletCount;

		public GunShotShotgun(GunShot wrappedShot, int bulletCount) : base(wrappedShot) {
			_bulletCount = bulletCount;
		}
		
		public override bool Available => true;
		
		public override void Execute() {
			for (int i = 0; i < _bulletCount; i++) {
				_wrappedShot.Execute();
			}
		}
	}
}