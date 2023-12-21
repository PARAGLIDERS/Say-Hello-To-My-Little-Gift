using UnityEngine;

namespace Dash {
	[CreateAssetMenu(menuName = "Santa/Player Dash Config")]
	public class PlayerDashConfig : ScriptableObject {
		[SerializeField] private float _magnitude;
		[SerializeField] private float _time;

		public float Magnitude => _magnitude;
		public float Time => _time;
	}
}
