using UnityEngine;

namespace DamageSystem {
	[CreateAssetMenu(menuName ="Santa/Damageable/Damage Trigger Config")]
	public class DamageTriggerConfig : ScriptableObject {
		[SerializeField] private float _damage;
	}
}
