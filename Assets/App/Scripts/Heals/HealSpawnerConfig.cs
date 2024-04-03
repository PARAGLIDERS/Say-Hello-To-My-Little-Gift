using UnityEngine;

namespace Heals {
	[CreateAssetMenu(menuName = "Santa/Heal/Heal Spawner Config")]
	public class HealSpawnerConfig : ScriptableObject {
		[SerializeField] private float _cooldown;

		public float Cooldown => _cooldown;
	}
}
