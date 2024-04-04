using Data;
using System.IO;
using UnityEditor;
using UnityEngine;
using Utils;

public static class DataCleaner {
	[MenuItem(Constants.MenuFolder + nameof(ClearData), false)]
	public static void ClearData() {
		if (File.Exists(DataService.DataPath)) {
			File.Delete(DataService.DataPath);
		}

		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();

		Debug.LogError("Data has been cleared");
	}
}
