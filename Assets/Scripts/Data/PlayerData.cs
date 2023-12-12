using System;

namespace Data {
	[Serializable]
	public class PlayerData {
		public LevelData LevelData { get; private set; }
		public SettingsData Settings { get; private set; }

		public PlayerData() {
			LevelData = new LevelData();
			Settings = new SettingsData();
		}

		public void LevelPassed() {
			LevelData.CurrentLevel++;
		}
	}
}
