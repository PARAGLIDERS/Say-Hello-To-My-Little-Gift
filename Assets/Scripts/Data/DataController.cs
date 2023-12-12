using System;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Data {
	public class DataController {
		public Action OnSave;

		public PlayerData Data { get; }
		private static string DataPath => Path.Combine(Application.persistentDataPath, "Player.data");

		public DataController() {
			Data = Load();
		}

		public void Dispose() {
			Save();
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

#if UNITY_EDITOR
		[MenuItem("Santa/Clear Data", false)]
#endif
		public static void Clear() {
			if (File.Exists(DataPath)) {
				File.Delete(DataPath);
			}

			PlayerPrefs.DeleteAll();
			PlayerPrefs.Save();

			Debug.LogError("Data has been cleared");
		}
	}
}
