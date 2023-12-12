using System;

namespace Data {
	[Serializable]
	public class PlayerData {
		public int CurrentLevel { get; private set; }
		public SettingsData Settings { get; private set; }

		public PlayerData() {
			CurrentLevel = 0;
			Settings = new SettingsData();
		}

		public void LevelPassed() {
			CurrentLevel++;
		}
	}
}
