using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace Data {
	public class DataService {
		public static string DataPath => Path.Combine(Application.persistentDataPath, "Player.data");

		public Action OnSave;
		public PlayerData Data { get; }

		public DataService() {
			Data = Load();
		}

		public void Save() {
			string s = JsonConvert.SerializeObject(Data);
			File.WriteAllText(DataPath, s);

			OnSave?.Invoke();
		}

		private PlayerData Load() {
			PlayerData data = new PlayerData();
			if (File.Exists(DataPath)) {
				string s = File.ReadAllText(DataPath);
				data = JsonConvert.DeserializeObject<PlayerData>(s);
			} else {
				string s = JsonConvert.SerializeObject(data);
				File.WriteAllText(DataPath, s);
			}

			return data;
		}

		public void ApplySettings() {
			OnSave?.Invoke();
		}
	}
}
