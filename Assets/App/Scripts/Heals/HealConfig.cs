using UnityEngine;

namespace Heals {
	[CreateAssetMenu(menuName = "Santa/Heal/Heal Config")]
	public class HealConfig : ScriptableObject {
		[SerializeField] private string _name;
		[SerializeField] private Color _color;
		[SerializeField] private int _value;

		public string Name => _name;
		public Color Color => _color;
		public int Value => _value;
	}
}
