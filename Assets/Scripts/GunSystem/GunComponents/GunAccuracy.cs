using UnityEngine;

namespace GunSystem.GunComponents {
	public abstract class GunAccuracy {
		public abstract float Value { get; }
	}

	public class GunAccuracyDefault : GunAccuracy {
		public override float Value => 10f;
	}
}