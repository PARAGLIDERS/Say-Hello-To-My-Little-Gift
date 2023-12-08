namespace GunSystem {
	public interface IGun {
		string Name { get; }
		bool IsInfinite { get; }
		int Ammo { get; }
	}
}
