using GunSystem;
using Player;
using Root;
using Ui.Screens;
using Ui.UiKit;
using UnityEngine;
using UnityEngine.UI;

namespace Cheat {
	public class CheatWindow : MonoBehaviour {
		[SerializeField] private GameObject _window;
		[SerializeField] private Button _close;

		[SerializeField] private UiToggle _god;
		[SerializeField] private UiToggle _ammo;
		[SerializeField] private UiToggle _hud;
		[SerializeField] private Button _guns;

		private const KeyCode _key = KeyCode.BackQuote;

		private bool _isActive;
		private float _timeScaleBeforeActivation;

		private void Awake() {
			_window.SetActive(false);

			_god.Init(PlayerDamageable.God, HandleGodToggle);
			_ammo.Init(Gun.Endless, HandleAmmoToggle);
			_hud.Init(UiScreenHud.Active, HandleHudToggle);
			_guns.onClick.AddListener(HandleGunsButton);

			_close.onClick.AddListener(Hide);
		}

		private void Update() {
			if (!Input.GetKeyDown(_key)) return;

			if (!_isActive) Show();
			else Hide();
		}

		private void Show() {
			_isActive = true;
			_window.SetActive(true);
			_timeScaleBeforeActivation = Time.timeScale;
			Time.timeScale = 0f;
		}

		private void Hide() {
			_isActive = false;
			_window.SetActive(false);
			Time.timeScale = _timeScaleBeforeActivation;
		}

		private void HandleGodToggle(bool value) {
			PlayerDamageable.God = value;
		}

		private void HandleAmmoToggle(bool value) {
			Gun.Endless = value;
		}

		private void HandleHudToggle(bool value) {
			UiScreenHud.Active = value;
		}

		private void HandleGunsButton() {
			foreach (object item in System.Enum.GetValues(typeof(GunType))) {
				Core.LevelController.GunsController.Pickup((GunType)item);
			}
		}
	}
}
