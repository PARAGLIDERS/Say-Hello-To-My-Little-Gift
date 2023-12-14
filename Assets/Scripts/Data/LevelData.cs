using System;

namespace Data {
	[Serializable]
	public class LevelData {
		public int CurrentLevel { get; set; }

		public LevelData() {
			CurrentLevel = 2;
		}
	}
}
