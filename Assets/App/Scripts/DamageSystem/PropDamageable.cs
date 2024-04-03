namespace DamageSystem {
	public class PropDamageable : Damageable {
		private void OnEnable() {
			OnDie += HandleDie;
		}

		private void OnDisable() {
			OnDie -= HandleDie;
		}

		private void HandleDie() {
			gameObject.SetActive(false);
		}
	}
}
