using UnityEngine;

namespace GunSystem.GunComponents {
	public abstract class GunInput {
		public abstract bool Handled { get; }
	}

	public class GunInputClick : GunInput {
		public override bool Handled => Input.GetMouseButtonDown(0);
	}

	public class GunInputHold : GunInput {
		public override bool Handled => Input.GetMouseButton(0);
	}
}