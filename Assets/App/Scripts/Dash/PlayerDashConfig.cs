using UnityEngine;

namespace Dash {
	[CreateAssetMenu(menuName = "Santa/Player Dash Config")]
	public class PlayerDashConfig : ScriptableObject {
		[SerializeField] private int _count;
		[SerializeField] private float _magnitude;
		[SerializeField] private float _cooldown;
		[SerializeField] private float _restore;

		public int Count => _count;
		public float Magnitude => _magnitude;
		public float Time => _cooldown;
		public float Restore => _restore;
	}
}
