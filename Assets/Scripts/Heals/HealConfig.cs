using UnityEngine;

namespace Heals {
	[CreateAssetMenu(menuName = "Santa/Heal/Heal Config")]
	public class HealConfig : ScriptableObject {
		[SerializeField] private int _value;

		public int Value => _value;
	}
}
